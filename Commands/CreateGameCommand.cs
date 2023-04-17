namespace RockPaperScissors.Commands
{

    using Enums;

    public class CreateGameCommand
    {
        public string Name { get; set; }

        public RockPaperScissors HeroChoice { get; set; }
    }
}
