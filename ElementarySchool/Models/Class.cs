namespace ElementarySchool.Models
{
    public class Class
    {
        public int ClassID { get; set; }
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
