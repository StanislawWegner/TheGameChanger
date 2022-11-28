using Microsoft.AspNetCore.Mvc;
using TheGameChanger.Models;
using TheGameChanger.Services;

namespace TheGameChanger.Controllers
{
    [Route("genre")]
    [ApiController]
    public class TypeOfGameController : ControllerBase
    {
        private readonly ITypeOfGameService _typeOfGameService;

        public TypeOfGameController(ITypeOfGameService typeOfGameService)
        {
            _typeOfGameService = typeOfGameService;
        }

        [HttpPost]
        public ActionResult<CreateTypOfGameDto> CreateTypeOfGame([FromBody] CreateTypOfGameDto dto)
        {
            var typeOfGameDto = _typeOfGameService.CreateTypeOfGame(dto);

            return Created($"genre/{typeOfGameDto}", typeOfGameDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TypeOfGameDto>> GetAllTypesOfGames()
        {
            return Ok(_typeOfGameService.GetAllTypesOfGamesDto());
        }

        [HttpDelete("deleteAll")]
        public ActionResult DeleteAllTypesOfGames()
        {
            _typeOfGameService.DeleteAllTypesOfGames();
            return Ok("All types of games deleted");
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        {
            _typeOfGameService.DeleteOneType(id);

            return Ok("Genre is deleted");
        }
        [HttpPut("update/{id}")]
        public ActionResult<TypeOfGameDto> UpdateTypeOfGame([FromRoute]int id, [FromBody]CreateTypOfGameDto dto)
        {
            var updatedType = _typeOfGameService.UpdateTypeOfGame(id, dto);

            return Created($"update/{id}", updatedType);
        }




    }
}
