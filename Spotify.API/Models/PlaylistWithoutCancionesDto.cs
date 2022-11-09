namespace Spotify.API.Models
{
    public class PlaylistWithoutCancionesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
