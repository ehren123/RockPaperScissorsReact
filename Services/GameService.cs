using Microsoft.EntityFrameworkCore;

namespace RockPaperScissors.Services
{
    using Commands;
    using Dtos;
    using Entities;
    using Enums;

    public class GameService : IGameService
    {
        private readonly RockPaperScissorsDbContext _context;

        public GameService(RockPaperScissorsDbContext context)
        {
            _context = context;
        }

        public async Task CreateGame(CreateGameCommand command)
        {
            var user = _context.Users
                .SingleOrDefault(u => u.Name == command.Name.Trim());

            // Add user when it doesn't already exist.
            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = command.Name.Trim(),
                    Created = DateTime.UtcNow
                };

                await _context.AddAsync(user);
            }

            var game = new Game
            {
                Id = Guid.NewGuid(),
                User = user,
                Created = DateTime.UtcNow,
                HeroChoice = command.HeroChoice,
                VillainChoice = GetComputerChoice()
            };

            SetGameResult(game);
            await _context.AddAsync(game);

            await UpdateUserStats(user, game);

            await _context.SaveChangesAsync();
        }

        public async Task<UserDto?> GetUserByName(string name)
        {
            var user = await _context.Users
                .Include(u => u.Games.OrderByDescending(g => g.Created).Take(1))
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Name == name.Trim());

            if (user == null)
            {
                return null;
            }

            var dto = new UserDto(user);

            // We add the results of the last game if it exists.
            if (user.Games.Any())
            {
                dto.LastGameHeroChoice = user.Games[0].HeroChoice;
                dto.LastGameVillainChoice = user.Games[0].VillainChoice;
                dto.LastGameResult = user.Games[0].Result;
            }

            return dto;
        }

        public async Task<List<UserDto>> GetLeaderBoard()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderByDescending(u => u.Score)
                .ThenByDescending(u => u.TotalGames)
                .Select(u => new UserDto(u))
                .ToListAsync();
        }

        private async Task UpdateUserStats(User user, Game game)
        {
            if (user.TotalGames % 20 == 0)
            {
                await PeriodicResync(user);
            }

            user.TotalGames++;

            switch (game.Result)
            {
                case GameResult.Win:
                    user.Score++;
                    user.Wins++;
                    break;
                case GameResult.Loss:
                    user.Score--;
                    user.Losses++;
                    break;
                case GameResult.Tie:
                    user.Ties++;
                    break;
            }
        }

        // Sometimes we need to resync the user stats to guarantee reliability.
        private async Task PeriodicResync(User user)
        {
            var stats = await _context.Games
                .AsNoTracking()
                .Where(g => g.UserId == user.Id)
                .GroupBy(g => g.UserId)
                .Select(g => new
                {
                    TotalGames = g.Count(),
                    Wins = g.Count(g => g.Result == GameResult.Win),
                    Losses = g.Count(g => g.Result == GameResult.Loss),
                    Ties = g.Count(g => g.Result == GameResult.Tie),
                    Score = g.Sum(g => g.Result == GameResult.Win ? 1 : g.Result == GameResult.Loss ? -1 : 0)
                })
                .SingleOrDefaultAsync();

            user.TotalGames = stats?.TotalGames ?? 0;
            user.Wins = stats?.Wins ?? 0;
            user.Losses = stats?.Losses ?? 0;
            user.Ties = stats?.Ties ?? 0;
            user.Score = stats?.Score ?? 0;
        }

        private RockPaperScissors GetComputerChoice()
        {
            var ran = new Random();
            return (RockPaperScissors)ran.Next(0, 3);
        }

        private void SetGameResult(Game game)
        {
            if (game.HeroChoice == game.VillainChoice)
            {
                game.Result = GameResult.Tie;
            }
            else if ((game.HeroChoice == RockPaperScissors.Rock && game.VillainChoice == RockPaperScissors.Scissors)
                       || (game.HeroChoice == RockPaperScissors.Paper && game.VillainChoice == RockPaperScissors.Rock)
                       || (game.HeroChoice == RockPaperScissors.Scissors && game.VillainChoice == RockPaperScissors.Paper))
            {
                game.Result = GameResult.Win;
            }
            else
            {
                game.Result = GameResult.Loss;
            }
        }
    }
}
