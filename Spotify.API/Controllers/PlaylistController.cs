using AutoMapper;
using Spotify.API.Models;
using Spotify.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Spotify.API.Controllers
{
    [ApiController]
    [Route("api/Playlists")]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistInfoRepository _PlaylistInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public PlaylistsController(IPlaylistInfoRepository PlaylistInfoRepository,
            IMapper mapper)
        {
            _PlaylistInfoRepository = PlaylistInfoRepository ??
                throw new ArgumentNullException(nameof(PlaylistInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistWithoutCancionesDto>>> GetPlaylists()
        {
            var PlaylistEntities = await _PlaylistInfoRepository.GetPlaylistsAsync();
            return Ok(_mapper.Map<IEnumerable<PlaylistWithoutCancionesDto>>(PlaylistEntities));

        }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistWithoutCancionesDto>>> GetPlaylists(
                string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }

            var (cityEntities, paginationMetadata) = await _PlaylistInfoRepository
                .GetPlaylistsAsync(name, searchQuery, pageNumber, pageSize);
            if (paginationMetadata != null)
            {
                Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            }
            

            return Ok(_mapper.Map<IEnumerable<PlaylistWithoutCancionesDto>>(cityEntities));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylist(
            int id, bool includeCanciones = false)
        {
            var Playlist = await _PlaylistInfoRepository.GetPlaylistAsync(id, includeCanciones);
            if (Playlist == null)
            {
                return NotFound();
            }

            if (includeCanciones)
            {
                return Ok(_mapper.Map<PlaylistDto>(Playlist));
            }

            return Ok(_mapper.Map<PlaylistWithoutCancionesDto>(Playlist));
        }

        
    }
}
