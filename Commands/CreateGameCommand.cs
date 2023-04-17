namespace RockPaperScissors.Commands
{

    using Enums;

    public class CreateGameCommand
    {
        public string Name { get; set; } = string.Empty;

        public RockPaperScissors HeroChoice { get; set; }
    }
}
