namespace RockPaperScissors.Commands
{

    using Enums;

    public class AddGameCommand
    {
        public string Name { get; set; }

        public GameResult HeroChoice { get; set; }
    }
}
