using System.ComponentModel.DataAnnotations;

namespace Demo1.Models
{
    public class PaymentProcess
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PaymentTitle { get; set; }

        [Required]
        public string Amount { get; set; }

        public List<Pay> pay { get; set; } = new List<Pay>();

    }
}
