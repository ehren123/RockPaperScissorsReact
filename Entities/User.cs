using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Ties { get; set; }

        public DateTime Created { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
