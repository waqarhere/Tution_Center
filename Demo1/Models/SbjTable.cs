using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Demo1.Models
{
    public class SbjTable
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }

        [DisplayName("TutorID:")]
        public string GeneratedTutorID { get; set; }

        [DisplayName("Tutor Name:")]
        public string tutor { get; set; }
        public string Subject { get; set; }

        [DisplayName("Tutor Availble Day:")]
        public string AvailbleDay { get; set; }

        [DisplayName("Tutor Availble Time:")]
        public string Availbletimest { get; set; }

        [DisplayName("Tutor Availble Time:")]
        public string Availbletimeend { get; set; }

        [ForeignKey("UserID")]
        public Addttr Addttr { get; set; }

        // New property to hold the list of subjects
        public List<string> SubjectList { get; set; } = new List<string>();
        public List<string> TutorList { get; set; } = new List<string>();
    }
}
