using Microsoft.AspNetCore.Mvc;
using TheGameChanger.Entities;
using TheGameChanger.Models;
using TheGameChanger.Services;

namespace TheGameChanger.Controllers
{
    [ApiController]
    [Route("game")]
    public class TheGameChangerController : ControllerBase
    {
        private readonly IGameService _gameService;

        public TheGameChangerController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameDto>> GetAllGames()
        {
            return Ok(_gameService.GetAll());
        }


        [HttpGet("gameName/{gameName}")]
        public ActionResult<GameDto> GetGameByName([FromRoute] string gameName)
        {
            return Ok(_gameService.GetByName(gameName));
        }

        [HttpPost("newGame")]
        public ActionResult CreateGame([FromBody] CreateGameDto dto)
        {
            var gameDto = _gameService.CreateGame(dto);

            return Created($"/game/gameName/{gameDto.Name}", gameDto);
        }

        [HttpDelete("deleteOneGame")]
        public ActionResult DeleteGame([FromBody] CreateGameDto dto)
        {
            _gameService.DeleteGame(dto);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<GameDto> UpdateGame([FromRoute] int id, [FromBody] CreateGameDto gameDto)
        {
            var updatedGame = _gameService.UpdateGame(id, gameDto);

            return Ok(updatedGame); 
        }

        [HttpDelete("deleteAll")]
        public ActionResult DeleteAllGames()
        {
            _gameService.DeleteAllGames();

            return Ok("Games deleted");
        }

        [HttpGet("CheckForOldGames")]
        public ActionResult<List<GameDto>> GamesOlderThan30Days()
        {
            var oldGames = _gameService.GamesOlderThan30Days();

            return Ok(oldGames);
        }

        [HttpGet("random")]
        public ActionResult<GameDto> RandomGame()
        {
           return Ok(_gameService.RandomGame());
        }
        [HttpPut("counter/{id}")]
        public ActionResult<CounterPointsDto> Counter([FromRoute]int id, [FromBody] AddPointsToCounterDto dto)
        {
            var counterPointsDto = _gameService.Counter(id, dto);
            return Ok(counterPointsDto);
        }
    }
}
