using System.ComponentModel.DataAnnotations;

namespace ElementarySchool.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        [Required(ErrorMessage ="EnrollmentDate is required")]
        public DateTime EnrollmentDate { get; set; }

        public int StudentID { get; set; }
        public int ClassID { get; set; }

        public Enrollment(int enrollmentID, DateTime enrollmentDate, int studentID, int classID)
        {
            EnrollmentID = enrollmentID;
            EnrollmentDate = enrollmentDate;
            StudentID = studentID;
            ClassID = classID;
        }
    }
}
