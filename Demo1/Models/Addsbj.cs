using System.ComponentModel.DataAnnotations;

namespace Demo1.Models
{
    public class Addsbj
    {
        [Key]
        public int Id { get; set; }
        public string Sujbect1 { get; set; }
        public string Program { get; set; }
    }
}
