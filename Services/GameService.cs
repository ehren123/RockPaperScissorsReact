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

            UpdateUserStats(user, game);

            await _context.SaveChangesAsync();
        }

        public async Task<UserDto?> GetUserByName(string name)
        {
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Name == name.Trim());

            if (user == null)
            {
                return null;
            }

            var lastGamePlayed = await _context.Games
                .AsNoTracking()
                .Where(g => g.UserId == user.Id)
                .OrderByDescending(g => g.Created)
                .FirstOrDefaultAsync();

            var dto = new UserDto(user);

            // We add the results of the last game if it exists.
            if (lastGamePlayed != null)
            {
                dto.LastGameHeroChoice = lastGamePlayed.HeroChoice;
                dto.LastGameVillainChoice = lastGamePlayed.VillainChoice;
                dto.LastGameResult = lastGamePlayed.Result;
            }

            return dto;
        }

        public async Task<List<UserDto>> GetLeaderBoard()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderByDescending(u => u.Score)
                .Select(u => new UserDto(u))
                .ToListAsync();
        }

        private void UpdateUserStats(User user, Game game)
        {
            user.TotalGames += 1;

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
