using RockPaperScissors.Entities;

namespace RockPaperScissors.Dtos
{
    public class GameStateDto
    {
        public string Name { get; set; }

        public Game? LastGame { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Ties { get; set; }

        public int TotalGames => Wins + Losses + Ties;

        public int Score => Wins - Losses;
    }
}
