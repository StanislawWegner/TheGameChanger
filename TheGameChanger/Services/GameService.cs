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
        GameDto GetByName(string gameName);
        void DeleteGame(CreateGameDto dto);
        GameDto UpdateGame(UpdateNameDto newGameDto);
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

        public GameDto GetByName(string gameName)
        {
            var game = _dbContext
                .Games
                .Include(g => g.TypeOfGame)
                .FirstOrDefault(g => g.Name.ToLower().Replace(" ", "") == gameName.ToLower().Replace(" ", ""));

            if (game is null)
                throw new NotFoundException("Gry nie znaleziono");

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
                throw new DataExistsException("Gra o takiej nazwie już istnieje");
            }
            
            var type = _dbContext
                .Types
                .ToList()
                .FirstOrDefault(t => t.Name.ToLower().Replace(" ", "") == dto.Type.ToLower().Replace(" ",""));

            if (type is null)
                throw new NotFoundException("Taki gatunek gry nie istnieje");

            var newGame = _mapper.Map<Game>(dto);
            newGame.TypeOfGameId = type.Id;
            _dbContext.Games.Add(newGame);

            _dbContext.SaveChanges();
            var gameDto = _mapper.Map<GameDto>(newGame);

            return gameDto;
        }

        public void DeleteGame(CreateGameDto dto)
        {
            var game = _dbContext.Games.FirstOrDefault
                (g => g.Name.ToLower().Replace(" ", "") == dto.Name.ToLower().Replace(" ", ""));

            if (game is null)
                throw new NotFoundException("Gra o takiej nazwie nie istnieje");

            var type = _dbContext.Types.FirstOrDefault
                (t => t.Name.ToLower().Replace(" ", "") == dto.Type.ToLower().Replace(" ", ""));

            if (type is null)
                throw new NotFoundException("Taki gatunek gry nie istnieje");

            if (game.TypeOfGameId != type.Id)
                throw new NotFoundException("Nazwa i gatunek nie należą do tej samej gry");

            _dbContext.Games.Remove(game);
            _dbContext.SaveChanges();
        }

        public GameDto UpdateGame(UpdateNameDto newGameNameDto)
        {
            var game = _dbContext.Games.Include(g => g.TypeOfGame)
                .FirstOrDefault(g => g.Name.ToLower().Replace(" ", "") == newGameNameDto.Name.ToLower().Replace(" ", ""));
            if (game is null) 
                throw new NotFoundException("Taka gra nie istnieje");

            var checkNewName = _dbContext.Games.FirstOrDefault
                (t => t.Name.ToLower().Replace(" ", "") == newGameNameDto.NewName.ToLower().Replace(" ", ""));

            if(checkNewName != null && checkNewName.Id != game.Id)
            {
                throw new DataExistsException("Gra o proponowanej nazwie już istnieje");
            }
            game.Name = newGameNameDto.NewName;

            _dbContext.SaveChanges();

            var gameDto = _mapper.Map<GameDto>(game);

            return gameDto;
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
