using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1.Models
{
    public class Pay
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int UserID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public List<string> TitleList { get; set; } = new List<string>();

        public List<PaymentProcess> PaymentProcesses { get; set; } = new List<PaymentProcess>();


    }
}
