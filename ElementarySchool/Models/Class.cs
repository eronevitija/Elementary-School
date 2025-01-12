using System.ComponentModel.DataAnnotations;

namespace ElementarySchool.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        public int TeacherID { get; set; }

        public Class(int classID, string title, int teacherID)
        {
            ClassID = classID;
            Title = title;
            TeacherID = teacherID;
        }
    }
}
