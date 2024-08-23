using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1.Models
{
    public class AddSubjectModel
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string GeneratedID { get; set; }
        public string Email { get; set; }
        public string Program { get; set; }
        public string Subject { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
