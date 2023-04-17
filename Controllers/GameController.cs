using Microsoft.AspNetCore.Mvc;

namespace RockPaperScissors.Controllers
{
    using Commands;
    using Dtos;
    using Services;

    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(CreateGameCommand command)
        {
            await _gameService.CreateGame(command);
            return Ok();
        }

        [HttpGet]
        [Produces(typeof(UserDto))]
        [Route("{name}")]
        public async Task<IActionResult> GetUserByName([FromRoute] string name)
        {
            var user = await _gameService.GetUserByName(name);
            return Ok(user);
        }
    }
}
