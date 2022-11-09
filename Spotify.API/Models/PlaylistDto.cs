namespace Spotify.API.Models
{
    public class PlaylistDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfCanciones
        {
            get
            {
                return Canciones.Count;
            }
        }

        public ICollection<CancionesDto> Canciones { get; set; }
            = new List<CancionesDto>();
    }
}
