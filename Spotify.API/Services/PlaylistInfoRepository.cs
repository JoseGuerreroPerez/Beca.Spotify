using Spotify.API.DbContexts;
using Spotify.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Spotify.API.Services
{
    public class PlaylistInfoRepository : IPlaylistInfoRepository
    {
        private readonly PlaylistInfoContext _context;

        public PlaylistInfoRepository(PlaylistInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync()
        {
            return await _context.Playlists.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Playlist?> GetPlaylistAsync(int playlistId, bool includeCanciones)
        {
            if (includeCanciones)
            {
                return await _context.Playlists.Include(c => c.Canciones)
                    .Where(c => c.Id == playlistId).FirstOrDefaultAsync();
            }

            return await _context.Playlists
                  .Where(c => c.Id == playlistId).FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<Playlist>, PaginationMetadata)> GetPlaylistsAsync(
    string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Playlists as IQueryable<Playlist>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }


        public async Task<bool> PlaylistExistsAsync(int playlistId)
        {
            return await _context.Playlists.AnyAsync(c => c.Id == playlistId);
        }

        public async Task<Cancion?> GetCancionForPlaylistAsync(
            int playlistId,
            int cancionId)
        {
            return await _context.Canciones
               .Where(p => p.PlaylistId == playlistId && p.Id == cancionId)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cancion>> GetCancionesForPlaylistAsync(
            int playlistId)
        {
            return await _context.Canciones
                           .Where(p => p.PlaylistId == playlistId).ToListAsync();
        }

        public async Task AddCancionForPlaylistAsync(int playlistId,
            Cancion Cancion)
        {
            var playlist = await GetPlaylistAsync(playlistId, false);
            if (playlist != null)
            {
                playlist.Canciones.Add(Cancion);
            }
        }

        public void DeleteCancion(Cancion Cancion)
        {
            _context.Canciones.Remove(Cancion);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
