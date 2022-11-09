using AutoMapper;

namespace Spotify.API.Profiles
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile()
        {
            CreateMap<Entities.Playlist, Models.PlaylistWithoutCancionesDto>();
            CreateMap<Entities.Playlist, Models.PlaylistDto>();
        }
    }
}
