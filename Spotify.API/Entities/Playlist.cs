using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spotify.API.Entities
{
    public class Playlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public ICollection<Cancion> Canciones { get; set; }
               = new List<Cancion>();

        public Playlist(string name)
        {
            Name = name;
        }
    }
}
