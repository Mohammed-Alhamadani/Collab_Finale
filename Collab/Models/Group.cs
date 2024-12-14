using System.ComponentModel.DataAnnotations;

namespace Collab.Models
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }

        [Required]
        public string GroupName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatorUserID { get; set; }
    }
}