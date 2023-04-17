using RockPaperScissors.Commands;
using RockPaperScissors.Dtos;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        Task CreateGame(CreateGameCommand command);

        Task<UserDto?> GetUserByName(string name);

        Task<List<UserDto>> GetLeaderBoard();
    }
}
