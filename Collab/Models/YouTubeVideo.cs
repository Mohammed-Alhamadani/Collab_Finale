using System.ComponentModel.DataAnnotations;

namespace Collab.Models
{
    public class YouTubeVideo
    {
        [Key]
        public int VideoID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string VideoURL { get; set; }

        public int CategoryID { get; set; }

        public int UploadedByUserID { get; set; }
    }
}
