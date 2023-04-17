namespace RockPaperScissors.Dtos
{
    using Entities;

    public class UserDto
    {
        public UserDto(User user)
        {
            Name = user.Name;
            Wins = user.Wins;
            Losses = user.Losses;
            Ties = user.Ties;
        }

        public string Name { get; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
    }
}
