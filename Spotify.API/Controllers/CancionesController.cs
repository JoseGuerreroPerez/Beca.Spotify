using AutoMapper;
using Spotify.API.Models;
using Spotify.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Spotify.API.Controllers
{
    [ApiController]
    [Route("api/playlist/{playlistId}/Canciones")]
    public class CancionesController : ControllerBase
    {
        private readonly ILogger<CancionesController> _logger;
        private readonly IPlaylistInfoRepository _playlistInfoRepository;
        private readonly IMapper _mapper;

        public CancionesController(ILogger<CancionesController> logger,
            IPlaylistInfoRepository playlistInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _playlistInfoRepository = playlistInfoRepository ??
                throw new ArgumentNullException(nameof(playlistInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CancionesDto>>> GetCanciones(
            int playlistId)
        {
            if (!await _playlistInfoRepository.PlaylistExistsAsync(playlistId))
            {
                _logger.LogInformation(
                    $"Playlist with id {playlistId} wasn't found when accessing canciones.");
                return NotFound();
            }

            var cancionesForPlaylist = await _playlistInfoRepository
                .GetCancionesForPlaylistAsync(playlistId);

            return Ok(_mapper.Map<IEnumerable<CancionesDto>>(cancionesForPlaylist));
        }

        [HttpGet("{cancionesid}")]
        public async Task<ActionResult<CancionesDto>> GetCanciones(
            int playlistId, int cancionesid)
        {
            if (!await _playlistInfoRepository.PlaylistExistsAsync(playlistId))
            {
                return NotFound();
            }

            var Cancion = await _playlistInfoRepository
                .GetCancionForPlaylistAsync(playlistId, cancionesid);

            if (Cancion == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CancionesDto>(Cancion));
        }

        [HttpPost]
        public async Task<ActionResult<CancionesDto>> CreateCancion(
           int playlistId,
           CancionesForCreationDto Cancion)
        {
            if (!await _playlistInfoRepository.PlaylistExistsAsync(playlistId))
            {
                return NotFound();
            }

            var finalCancion = _mapper.Map<Entities.Cancion>(Cancion);

            await _playlistInfoRepository.AddCancionForPlaylistAsync(
                playlistId, finalCancion);

            await _playlistInfoRepository.SaveChangesAsync();

            var createdCancionToReturn =
                _mapper.Map<Models.CancionesDto>(finalCancion);

            return Ok(_mapper.Map<CancionesDto>(createdCancionToReturn)); ;
        }

        [HttpPut("{Cancionid}")]
        public async Task<ActionResult> UpdateCancion(int playlistId, int Cancionid,
            CancionesForUpdateDto Cancion)
        {
            if (!await _playlistInfoRepository.PlaylistExistsAsync(playlistId))
            {
                return NotFound();
            }

            var CancionEntity = await _playlistInfoRepository
                .GetCancionForPlaylistAsync(playlistId, Cancionid);
            if (CancionEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(Cancion, CancionEntity);

            await _playlistInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{Cancionid}")]
        public async Task<ActionResult> PartiallyUpdateCancion(
            int playlistId, int Cancionid,
            JsonPatchDocument<CancionesForUpdateDto> patchDocument)
        {
            if (!await _playlistInfoRepository.PlaylistExistsAsync(playlistId))
            {
                return NotFound();
            }

            var CancionEntity = await _playlistInfoRepository
                .GetCancionForPlaylistAsync(playlistId, Cancionid);
            if (CancionEntity == null)
            {
                return NotFound();
            }

            var CancionToPatch = _mapper.Map<CancionesForUpdateDto>(
                CancionEntity);

            patchDocument.ApplyTo(CancionToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(CancionToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(CancionToPatch, CancionEntity);
            await _playlistInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{CancionId}")]
        public async Task<ActionResult> DeleteCancion(
            int playlistId, int CancionId)
        {
            if (!await _playlistInfoRepository.PlaylistExistsAsync(playlistId))
            {
                return NotFound();
            }

            var CancionEntity = await _playlistInfoRepository
                .GetCancionForPlaylistAsync(playlistId, CancionId);
            if (CancionEntity == null)
            {
                return NotFound();
            }

            _playlistInfoRepository.DeleteCancion(CancionEntity);
            await _playlistInfoRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
