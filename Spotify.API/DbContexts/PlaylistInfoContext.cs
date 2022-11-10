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
               new Playlist("lista coche")
               {
                   Id = 1,
                   Description = "Canciones para escuchar en el coche."
               },
               new Playlist("Concentración")
               {
                   Id = 2,
                   Description = "lista de canciones de piano para concentrarse."
               },
               new Playlist("Más escuchadas")
               {
                   Id = 3,
                   Description = "Lista de 50 canciones más escuchadas."
               });

            modelBuilder.Entity<Cancion>()
             .HasData(
               new Cancion("I want it all")
               {
                   Id = 1,
                   PlaylistId = 1,
                   Description = "Queen."
               },
               new Cancion("Vino tinto")
               {
                   Id = 2,
                   PlaylistId = 1,
                   Description = "Estopa."
               },
                 new Cancion("piano1")
                 {
                     Id = 3,
                     PlaylistId = 2,
                     Description = "choppin."
                 },
               new Cancion("piano2")
               {
                   Id = 4,
                   PlaylistId = 2,
                   Description = "choppin."
               },
               new Cancion("Tacones rojos")
               {
                   Id = 5,
                   PlaylistId = 3,
                   Description = "Sebastián Yatra."
               },
               new Cancion("La fama")
               {
                   Id = 6,
                   PlaylistId = 3,
                   Description = "Rosalia."
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
