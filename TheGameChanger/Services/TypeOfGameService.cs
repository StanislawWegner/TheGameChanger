﻿using AutoMapper;
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
        TypeOfGameDto UpdateTypeOfGame(UpdateNameDto newTypeNamedto);
        TypeOfGameDto GetTypeByName(string typeName);
        IEnumerable<GameDto> ListOfGamesForOneType(string typeName);
        int QuantityOfGamesForOneType(string typeName);
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
            var checkType = _dbContext.Types
                .FirstOrDefault(t => t.Name.ToLower().Replace(" ", "") == dto.Name.ToLower().Replace(" ", ""));

            if (checkType != null)
                throw new DataExistsException("Podany gatunek gry już istnieje");

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
            var type = _dbContext.Types
                .FirstOrDefault(t => t.Name.ToLower().Replace(" ", "") == typeName.ToLower().Replace(" ", ""));

            if (type is null)
            {
                throw new NotFoundException("Taki gatunek nie istnieje");
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
            var type = _dbContext
                .Types
                .FirstOrDefault
                (t => t.Name.ToLower().Replace(" ", "") == typeName.ToLower().Replace(" ",""));
            if (type is null)
                throw new NotFoundException("Taki gatunek nie istnieje");
            
            _dbContext.Remove(type);
            _dbContext.SaveChanges();
        }

        public IEnumerable<GameDto> ListOfGamesForOneType(string typeName)
        {
            var type = _dbContext
                .Types
                .Include(t => t.Games)
                .FirstOrDefault
                (t => t.Name.ToLower().Replace(" ", "") == typeName.ToLower().Replace(" ", ""));

            if (type is null)
                throw new NotFoundException("Taki gatunek nie istnieje");

            var games =  type.Games.ToList();

            var gamesDto = _mapper.Map<List<GameDto>>(games);
            return gamesDto;
        }

        public int QuantityOfGamesForOneType(string typeName)
        {
            var type = _dbContext
               .Types
               .Include(t => t.Games)
               .FirstOrDefault
               (t => t.Name.ToLower().Replace(" ", "") == typeName.ToLower().Replace(" ", ""));

            if (type is null)
                throw new NotFoundException("Taki gatunek nie istnieje");

            var gamesQuantity = type.Games.Count;

            return gamesQuantity;
        }

        public TypeOfGameDto UpdateTypeOfGame(UpdateNameDto newTypeNameDto)
        {
            var typeToUpdate = _dbContext
                .Types
                .Include(t => t.Games)
                .FirstOrDefault
                (t => t.Name.ToLower().Replace(" ", "") == newTypeNameDto.Name.ToLower().Replace(" ",""));

            if (typeToUpdate is null)
                throw new NotFoundException("Taki gatunek nie istnieje");

            var checkNewName = _dbContext.Types.FirstOrDefault
                (t => t.Name.ToLower().Replace(" ", "") == newTypeNameDto.NewName.ToLower().Replace(" ", ""));

            
            if(checkNewName != null && checkNewName.Id != typeToUpdate.Id)
            {
                throw new DataExistsException("Gatunek o proponowanej nazwie już istnieje");
            }
            typeToUpdate.Name = newTypeNameDto.NewName;

            _dbContext.SaveChanges();
            return _mapper.Map<TypeOfGameDto>(typeToUpdate);
        }
    }
}
