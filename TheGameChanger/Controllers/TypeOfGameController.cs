using Microsoft.AspNetCore.Mvc;
using TheGameChanger.Models;
using TheGameChanger.Services;

namespace TheGameChanger.Controllers
{
    [Route("type")]
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

            return Created($"type/typeName/{typeOfGameDto.Name}", typeOfGameDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TypeOfGameDto>> GetAllTypesOfGames()
        {
            return Ok(_typeOfGameService.GetAllTypesOfGamesDto());
        }

        [HttpGet("typeName/{typeName}")]
        public ActionResult<TypeOfGameDto> GetTypeByName([FromRoute] string typeName)
        {
            return Ok(_typeOfGameService.GetTypeByName(typeName));
        }

        [HttpDelete("deleteAll")]
        public ActionResult DeleteAllTypesOfGames()
        {
            _typeOfGameService.DeleteAllTypesOfGames();
            return Ok("All types of games deleted");
        }

        [HttpDelete("deleteOneType/{typeName}")]
        public ActionResult DeleteByTypName([FromRoute] string typeName)
        {
            _typeOfGameService.DeleteOneType(typeName);

            return NoContent();
        }

        [HttpGet("gameList/{typeName}")]
        public ActionResult<IEnumerable<GameDto>> GetListOfGamesForOneType([FromRoute] string typeName)
        {
            var result = _typeOfGameService.ListOfGamesForOneType(typeName);

            return Ok(result);
        }

        [HttpGet("gameQuantity/{typeName}")]
        public ActionResult<int> GetQuantityOfGamesForOneType([FromRoute] string typeName)
        {
            var result = _typeOfGameService.QuantityOfGamesForOneType(typeName);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public ActionResult<TypeOfGameDto> UpdateTypeOfGame([FromRoute]int id, [FromBody]CreateTypOfGameDto dto)
        {
            var updatedType = _typeOfGameService.UpdateTypeOfGame(id, dto);

            return Created($"update/{id}", updatedType);
        }




    }
}
