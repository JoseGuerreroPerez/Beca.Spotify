using AutoMapper;

namespace Spotify.API.Profiles
{
    public class CancionesProfile : Profile
    {
        public CancionesProfile()
        {
            CreateMap<Entities.Cancion, Models.CancionesDto>();
            CreateMap<Models.CancionesForCreationDto, Entities.Cancion>();
            CreateMap<Models.CancionesForUpdateDto, Entities.Cancion>();
            CreateMap<Entities.Cancion, Models.CancionesForUpdateDto>();
        }
    }
}
