using System.ComponentModel.DataAnnotations;

namespace Collab.Models
{
    public class YouTubeCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
