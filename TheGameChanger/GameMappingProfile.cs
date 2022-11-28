using AutoMapper;
using TheGameChanger.Entities;
using TheGameChanger.Models;

namespace TheGameChanger
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Game, GameDto>()
                .ForMember(m => m.Genre, c => c.MapFrom(s => s.TypeOfGame.Name));

            CreateMap<TypeOfGame, TypeOfGameDto>();

            CreateMap<CreateGameDto, Game>();

            CreateMap<CreateTypOfGameDto, TypeOfGame>();
            CreateMap<Game, CounterPointsDto>();

        }
    }
}
