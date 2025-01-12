using System;
using System.ComponentModel.DataAnnotations;


namespace ElementarySchool.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNo is required")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Subject is required")]

        public string Subject{ get; set; }
        [Required(ErrorMessage = "Address is required")]

        public string Address { get; set; }

        [Required(ErrorMessage = "DateOfHire is required")]
        public DateTime? DateOfHire { get; set; }
        [Required(ErrorMessage ="IsActive is required")]
        public bool IsActive { get; set; }

        public Teacher(int teacherID,string firstName, string lastName, string email, string phoneNo,
            string subject, string address, DateTime? dateOfHire, bool isActive)
        {
            TeacherID = teacherID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNo = phoneNo;
            Subject = subject;
            Address = address;
            DateOfHire = dateOfHire;
            IsActive = isActive;
        }

    }
}