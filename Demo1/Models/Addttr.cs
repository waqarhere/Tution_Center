using Demo1.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo1.Models
{
    public class Addttr
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string GeneratedID { get; set; }
        public string Email { get; set; }
        public string Program { get; set; }
        public string Subject { get; set; }
        public string GeneratedTutorID { get; set; }
        public string tutor { get; set; }

        [DisplayName("Tutor Availble Day:")]
        public string AvailbleDay { get; set; }

        [DisplayName("Tutor Availble Time:")]
        public string Availbletimest { get; set; }

        [DisplayName("Tutor Availble Time:")]
        public string Availbletimeend { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        // New property to hold the list of subjects
        public List<string> SubjectList { get; set; } = new List<string>();
        public List<string> TutorList { get; set; } = new List<string>();
    }
}
