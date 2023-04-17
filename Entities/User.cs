namespace RockPaperScissors.Entities
{
    /*
     * Although we could compute the score and total games from the wins, losses, and ties, we wish to be able to sort with this information in the database.
     * We also keep track of the wins, losses, and ties to avoid having to query the database for all games for a user.
     */
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Ties { get; set; }

        public int Score { get; set; }

        public int TotalGames { get; set; }

        public DateTime Created { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
