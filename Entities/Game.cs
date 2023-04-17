namespace RockPaperScissors.Entities
{
    using Enums;

    public class Game
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public RockPaperScissors HeroChoice { get; set; }

        public RockPaperScissors VillainChoice { get; set; }

        public DateTime Created { get; set; }

        public GameResult Result { get; set; }

        public User User { get; set; } = null!;
    }
}
