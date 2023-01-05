using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGameChanger.Entities;
using TheGameChanger.Exceptions;
using TheGameChanger.Models;

namespace TheGameChanger.Services
{
    public interface ITypeOfGameService
    {
        TypeOfGameDto CreateTypeOfGame(CreateTypOfGameDto dto);
        void DeleteAllTypesOfGames();
        void DeleteOneType(string typeName);
        List<TypeOfGameDto> GetAllTypesOfGamesDto();
        TypeOfGameDto UpdateTypeOfGame(int id, CreateTypOfGameDto dto);
        TypeOfGameDto GetTypeByName(string typeName);
        IEnumerable<GameDto> QuantityOfGamesInOneType(string typeName);
    }

    public class TypeOfGameService : ITypeOfGameService
    {
        private readonly GameDbContext _dbContext;
        private readonly IMapper _mapper;

        public TypeOfGameService(GameDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public TypeOfGameDto CreateTypeOfGame(CreateTypOfGameDto dto)
        {
            var typeOfGame = _mapper.Map<TypeOfGame>(dto);
            _dbContext.Types.Add(typeOfGame);
            _dbContext.SaveChanges();

            var responseTypeOfGameDto = _mapper.Map<TypeOfGameDto>(typeOfGame);

            return responseTypeOfGameDto;
        }

        public List<TypeOfGameDto> GetAllTypesOfGamesDto()
        {
            var listOfTypes = _dbContext.Types.ToList();
            var listOfTypesDto = _mapper.Map<List<TypeOfGameDto>>(listOfTypes);

            return listOfTypesDto;
        }

        public TypeOfGameDto GetTypeByName(string typeName)
        {
            var type = _dbContext.Types.FirstOrDefault(t => t.Name.ToLower() == typeName.ToLower());

            if(type is null)
            {
                throw new NotFoundException("Type not found");
            }

           var result = _mapper.Map<TypeOfGameDto>(type);
            
           return result;
        }

        public void DeleteAllTypesOfGames()
        {
            _dbContext.RemoveRange(_dbContext.Types);
            _dbContext.SaveChanges();
        }

        public void DeleteOneType(string typeName)
        {
            var type = _dbContext.Types.FirstOrDefault
                (t => t.Name.ToLower().Replace(" ", "") == typeName.ToLower().Replace(" ",""));
            if (type is null)
                throw new NotFoundException("Type was not found");

            
            _dbContext.Remove(type);
            _dbContext.SaveChanges();
        }

        public IEnumerable<GameDto> QuantityOfGamesInOneType(string typeName)
        {
            var type = _dbContext
                .Types
                .Include(t => t.Games)
                .FirstOrDefault
                (t => t.Name.ToLower().Replace(" ", "") == typeName.ToLower().Replace(" ", ""));

            if (type is null)
                throw new NotFoundException("Type was not found");

            //var game = _dbContext
            //    .Games
            //    .Include(g => g.TypeOfGame)
            //    .Where(g => g.TypeOfGameId == type.Id);

            var games =  type.Games.ToList();

            var gamesDto = _mapper.Map<List<GameDto>>(games);
            return gamesDto;
        }

        public TypeOfGameDto UpdateTypeOfGame(int id, CreateTypOfGameDto dto)
        {
            var typeToUpdate = _dbContext.Types.FirstOrDefault(t => t.Id == id);
            if (typeToUpdate is null)
                throw new NotFoundException("Type was not found");

            typeToUpdate.Name = dto.Name;
            var updatedType = _mapper.Map<TypeOfGame>(typeToUpdate);

            _dbContext.SaveChanges();
            return _mapper.Map<TypeOfGameDto>(updatedType);
        }
    }
}
