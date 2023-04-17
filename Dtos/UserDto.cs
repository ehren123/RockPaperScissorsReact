namespace RockPaperScissors.Dtos
{
    using Entities;
    using Enums;

    public class UserDto
    {
        public UserDto(User user)
        {
            Name = user.Name;
            Wins = user.Wins;
            Losses = user.Losses;
            Ties = user.Ties;
            TotalGames = user.TotalGames;
            Score = user.Score;
        }

        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Wins
        /// </summary>
        public int Wins { get; }


        /// <summary>
        /// Losses
        /// </summary>
        public int Losses { get; }


        /// <summary>
        /// Ties
        /// </summary>
        public int Ties { get; }

        /// <summary>
        /// Total games played
        /// </summary>
        public int TotalGames { get; }

        /// <summary>
        /// Total score
        /// </summary>
        public int Score { get; }

        /// <summary>
        /// Hero selection for last game.
        /// </summary>
        public RockPaperScissors? LastGameHeroChoice { get; set; }

        /// <summary>
        /// Villain selection for last game.
        /// </summary>
        public RockPaperScissors? LastGameVillainChoice { get; set; }

        /// <summary>
        /// Result of last game played.
        /// </summary>
        public GameResult? LastGameResult { get; set; }
    }
}
