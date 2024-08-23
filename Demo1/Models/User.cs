namespace Demo1.Models
{
    public class User
    {
        // Common fields
        public int Id { get; set; }
        public string GeneratedID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Role { get; set; } // Role to determine if the user is a student or a tutor
        public string Password { get; set; }
        public string Verfication { get; set; }
        public string Programs { get; set; }

        // Student-specific fields
        public string FatherName { get; set; }
        public string FatherMobile { get; set; }
        public string PicturePath { get; set; }
        public string MatricMarksheetPath { get; set; }
        public string MatricCertificatePath { get; set; }
        public string InterMarksheetPath { get; set; }
        public string InterCertificatePath { get; set; }
        public string CnicPath { get; set; }

        // Tutor-specific fields
        public string LastQualification { get; set; }
        public string Experience { get; set; }
        public string SubjectArea { get; set; }
        public string SubjectAreaExpertise { get; set; }
        public string BachelorsDegreePath { get; set; }
        public string MastersDegreePath { get; set; }
        public string CvPath { get; set; }
        public string avlDay { get; set; }
        public string avlTimestrt { get; set; }
        public string avlTimeend { get; set; }

        public virtual ICollection<AddSubjectModel> Subjects { get; set; }
    }

}
