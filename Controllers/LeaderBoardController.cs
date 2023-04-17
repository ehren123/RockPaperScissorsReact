using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Services;

namespace RockPaperScissors.Controllers
{
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
        public async Task<IActionResult> GetLeaderBoard()
        {
            var leaderBoard = await _gameService.GetLeaderBoard();
            return Ok(leaderBoard);
        }
    }
}
