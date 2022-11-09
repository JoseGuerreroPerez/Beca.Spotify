using Spotify.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Spotify.API.DbContexts
{
    public class PlaylistInfoContext : DbContext
    {
        public DbSet<Playlist> Playlists { get; set; } = null!;
        public DbSet<Cancion> Canciones { get; set; } = null!;

        public PlaylistInfoContext(DbContextOptions<PlaylistInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>()
                .HasData(
               new Playlist("New York City")
               {
                   Id = 1,
                   Description = "The one with that big park."
               },
               new Playlist("Antwerp")
               {
                   Id = 2,
                   Description = "The one with the cathedral that was never really finished."
               },
               new Playlist("Paris")
               {
                   Id = 3,
                   Description = "The one with that big tower."
               });

            modelBuilder.Entity<Cancion>()
             .HasData(
               new Cancion("Central Park")
               {
                   Id = 1,
                   PlaylistId = 1,
                   Description = "The most visited urban park in the United States."
               },
               new Cancion("Empire State Building")
               {
                   Id = 2,
                   PlaylistId = 1,
                   Description = "A 102-story skyscraper located in Midtown Manhattan."
               },
                 new Cancion("Cathedral")
                 {
                     Id = 3,
                     PlaylistId = 2,
                     Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
                 },
               new Cancion("Antwerp Central Station")
               {
                   Id = 4,
                   PlaylistId = 2,
                   Description = "The the finest example of railway architecture in Belgium."
               },
               new Cancion("Eiffel Tower")
               {
                   Id = 5,
                   PlaylistId = 3,
                   Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
               },
               new Cancion("The Louvre")
               {
                   Id = 6,
                   PlaylistId = 3,
                   Description = "The world's largest museum."
               }
               );
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
