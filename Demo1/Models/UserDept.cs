using System.ComponentModel.DataAnnotations;

namespace Demo1.Models
{
    public class UserDept
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
