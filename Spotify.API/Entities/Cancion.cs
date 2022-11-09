using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spotify.API.Entities
{
    public class Cancion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }


        [ForeignKey("PlaylistId")]
        public Playlist? Playlist { get; set; }
        public int PlaylistId { get; set; }

        public Cancion(string name)
        {
            Name = name;
        }
    }
}
