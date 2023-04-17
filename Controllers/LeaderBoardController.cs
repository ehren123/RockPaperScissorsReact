using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Services;

namespace RockPaperScissors.Controllers
{
    using Dtos;

    [ApiController]
    [Route("api/[controller]")]
    public class LeaderBoardController : ControllerBase
    {
        private readonly IGameService _gameService;

        public LeaderBoardController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [Produces(typeof(List<UserDto>))]
        public async Task<IActionResult> GetLeaderBoard()
        {
            var users = await _gameService.GetLeaderBoard();
            return Ok(users);
        }
    }
}
