using RockPaperScissors.Commands;

namespace RockPaperScissors.Services
{
    using Entities;
    using Enums;

    public class GameService
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
                    Name = command.Name,
                    Created = DateTime.UtcNow,
                    Id = Guid.NewGuid()
                };

                await _context.AddAsync(user);
            }

            var game = new Game
            {
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

        private void UpdateUserStats(User user, Game game)
        {
            switch (game.Result)
            {
                case GameResult.Win:
                    user.Wins++;
                    break;
                case GameResult.Loss:
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
            return (RockPaperScissors)ran.Next(0, 4);
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
