using Spotify.API.Models;

namespace Spotify.API
{
    public class SpotifyDataStore
    {
        public List<PlaylistDto> Playlist { get; set; }
       // public static PlaylistDataStore Current { get; } = new PlaylistDataStore();

        public SpotifyDataStore()
        {
            // init dummy data
            Playlist = new List<PlaylistDto>()
            {
                new PlaylistDto()
                {
                     Id = 1,
                     Name = "New York City",
                     Description = "The one with that big park.",
                     Canciones = new List<CancionesDto>()
                     {
                         new CancionesDto() {
                             Id = 1,
                             Name = "Central Park",
                             Description = "The most visited urban park in the United States." },
                          new CancionesDto() {
                             Id = 2,
                             Name = "Empire State Building",
                             Description = "A 102-story skyscraper located in Midtown Manhattan." },
                     }
                },
                new PlaylistDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished.",
                    Canciones = new List<CancionesDto>()
                     {
                         new CancionesDto() {
                             Id = 3,
                             Name = "Cathedral of Our Lady",
                             Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans." },
                          new CancionesDto() {
                             Id = 4,
                             Name = "Antwerp Central Station",
                             Description = "The the finest example of railway architecture in Belgium." },
                     }
                },
                new PlaylistDto()
                {
                    Id= 3,
                    Name = "Paris",
                    Description = "The one with that big tower.",
                    Canciones = new List<CancionesDto>()
                     {
                         new CancionesDto() {
                             Id = 5,
                             Name = "Eiffel Tower",
                             Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel." },
                          new CancionesDto() {
                             Id = 6,
                             Name = "The Louvre",
                             Description = "The world's largest museum." },
                     }
                }
            };

        }

    }
}
