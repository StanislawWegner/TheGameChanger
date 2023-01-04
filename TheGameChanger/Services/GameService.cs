using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGameChanger.Entities;
using TheGameChanger.Models;
using TheGameChanger.Exceptions;

namespace TheGameChanger.Services
{
    public interface IGameService
    {
        IEnumerable<GameDto> GetAll();
        GameDto CreateGame(CreateGameDto dto);
        GameDto GetById(int id);

        GameDto GetByName(string gameName);
        void DeleteGame(int id);
        GameDto UpdateGame(int id, CreateGameDto gameDto);
        void DeleteAllGames();
        List<GameDto> GamesOlderThan30Days();
        GameDto RandomGame();
        CounterPointsDto Counter(int id, AddPointsToCounterDto dto);
    }

    public class GameService : IGameService
    {
        private readonly GameDbContext _dbContext;
        private readonly IMapper _mapper;

        public GameService(GameDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<GameDto> GetAll()
        {
           
            var games = _dbContext
                .Games
                .Include(g => g.TypeOfGame)
                .ToList();

            var gamesDto = _mapper.Map<List<GameDto>>(games);
            return gamesDto;
        }

        public GameDto GetById(int id)
        {
            var game = _dbContext
                .Games
                .Include(g => g.TypeOfGame)
                .FirstOrDefault(g => g.Id == id);

            if (game is null)
                throw new NotFoundException("Game not found");

            var result = _mapper.Map<GameDto>(game);

            return result;
        }

        public GameDto GetByName(string gameName)
        {
            var game = _dbContext
                .Games
                .Include(g => g.TypeOfGame)
                .FirstOrDefault(g => g.Name.ToLower().Replace(" ", "") == gameName.ToLower().Replace(" ", ""));

            if (game is null)
                throw new NotFoundException("Game not found");

            var result = _mapper.Map<GameDto>(game);

            return result;
        }

        public GameDto CreateGame(CreateGameDto dto)
        {
            var game = _dbContext
                .Games
                .FirstOrDefault(g => g.Name.ToLower().Replace(" ","") == dto.Name.ToLower().Replace(" ", ""));

            if(game != null)
            {
                throw new DataExistsException("Game with that name already exists");
            }
            
            var type = _dbContext
                .Types
                .ToList()
                .FirstOrDefault(t => t.Name.ToLower().Replace(" ", "") == dto.Type.ToLower().Replace(" ",""));

            if (type is null)
                throw new NotFoundException("This type of game do not exist");

            var newGame = _mapper.Map<Game>(dto);
            newGame.TypeOfGameId = type.Id;
            _dbContext.Games.Add(newGame);

            _dbContext.SaveChanges();
            var gameDto = _mapper.Map<GameDto>(newGame);

            return gameDto;
        }

        public void DeleteGame(int id)
        {
            var game = _dbContext.Games.FirstOrDefault(g => g.Id == id);

            if (game is null)
                throw new NotFoundException("Game not found");

            _dbContext.Games.Remove(game);
            _dbContext.SaveChanges();
        }

        public GameDto UpdateGame(int id, CreateGameDto gameDto)
        {
            var game = _dbContext.Games.FirstOrDefault(g => g.Id == id);
            if (game is null) 
                throw new NotFoundException("Game not found");

            var updatedGame = _mapper.Map<CreateGameDto, Game>(gameDto, game);

            var GameAsAResponse = _mapper.Map<GameDto>(updatedGame);

            _dbContext.SaveChanges();

            return GameAsAResponse;
        }

        public void DeleteAllGames()
        {
            _dbContext.RemoveRange(_dbContext.Games);
            _dbContext.SaveChanges();
        }

        public List<GameDto> GamesOlderThan30Days()
        {
            var timespan = TimeSpan.FromDays(30);
            var expiredDate = DateTime.Now - timespan;
            var listOfOldGames = _dbContext.Games
                .Where(g => g.CreatedDate < expiredDate)
                .Include(g => g.TypeOfGame)
                .ToList();

            if (!listOfOldGames.Any())
            {
                throw new NotFoundException("No games older than 30 days");
            }

            var listOfGamesOlderThan30Days = _mapper.Map<List<GameDto>>(listOfOldGames);
            return listOfGamesOlderThan30Days;
        }

        public GameDto RandomGame()
        {
            var listOfGames = _dbContext
                .Games
                .Include(t => t.TypeOfGame)
                .ToList();

            var numbersOfGames = listOfGames.Count();
            Random random = new Random();

            var randomNumber = random.Next(numbersOfGames);
            var randomGame = listOfGames[randomNumber];

            return _mapper.Map<GameDto>(randomGame);
        }

        public CounterPointsDto Counter(int id, AddPointsToCounterDto dto)
        {
            var game = _dbContext.Games.FirstOrDefault(g => g.Id == id);
            if (game is null)
                throw new NotFoundException("Game not found");

            
            if(dto.Counter == "plus")
            {
                game.PositiveCounter++;
            }
            if(dto.Counter == "minus")
            {
                game.NegativeCounter += 1;
            }
            if(dto.Counter != "plus" && dto.Counter != "minus")
            {
                throw new NotFoundException("Write plus or minus to add points to a game");
            }
            _dbContext.SaveChanges();

            var gameDtoWithCounters = _mapper.Map<CounterPointsDto>(game);

            return gameDtoWithCounters;
            
        }

    }
}
