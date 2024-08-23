using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1.Models
{
    public class InternalUser
    {
        [Key]
        public int InternalID { get; set; }

        public int DepartmentID { get; set; }


        [ForeignKey("DepartmentID")]
        public UserDept Department { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
