using System.ComponentModel.DataAnnotations;

namespace ElementarySchool.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "FatherName is required")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime? Birthdate { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "PhoneNo is required")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "EnrollmentDate is required")]
        public DateTime? EnrollmentDate { get; set; }

        [Required(ErrorMessage ="IsActive is required")]
        public bool IsActive { get; set; }


        public Student(int studentID, string firstName, string fatherName,string lastName, string gender,
            DateTime? birthdate, string address ,string phoneNo, string email, DateTime? enrollmentDate, bool isActive)
        {
            StudentID = studentID;
            FirstName = firstName;
            FatherName = fatherName;
            LastName = lastName;    
            Gender = gender;
            Birthdate = birthdate;
            PhoneNo = phoneNo;
            Email = email;
            Address = address;
            EnrollmentDate  = enrollmentDate;
            IsActive = isActive;
        }

    }
}
