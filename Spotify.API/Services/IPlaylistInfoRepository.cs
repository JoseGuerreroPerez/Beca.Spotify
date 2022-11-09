using Spotify.API.Entities;

namespace Spotify.API.Services
{
    public interface IPlaylistInfoRepository
    {
        Task<IEnumerable<Playlist>> GetPlaylistsAsync();

        Task<(IEnumerable<Playlist>, PaginationMetadata)> GetPlaylistsAsync(
string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Playlist?> GetPlaylistAsync(int PlaylistId, bool includeCanciones);
        Task<bool> PlaylistExistsAsync(int PlaylistId);
        Task<IEnumerable<Cancion>> GetCancionesForPlaylistAsync(int PlaylistId);
        Task<Cancion?> GetCancionForPlaylistAsync(int PlaylistId,
            int CancionId);
        Task AddCancionForPlaylistAsync(int PlaylistId, Cancion Cancion);
        void DeleteCancion(Cancion Cancion);
        Task<bool> SaveChangesAsync();
    }
}
