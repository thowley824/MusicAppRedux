using System.ComponentModel.DataAnnotations;

namespace MusicAppRedux.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }

        [Required(ErrorMessage ="Artist name is required")]
        public string Name { get; set; }

        public string Bio { get; set; }
    }
}