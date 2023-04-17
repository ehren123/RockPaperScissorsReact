namespace RockPaperScissors.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Ties { get; set; }

        public DateTime Created { get; set; }
    }
}
