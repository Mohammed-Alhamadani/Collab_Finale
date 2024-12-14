using System.ComponentModel.DataAnnotations;

namespace Collab.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public int GroupID { get; set; }

        public string Type { get; set; }
    }
}
