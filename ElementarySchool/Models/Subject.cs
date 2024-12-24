namespace ElementarySchool.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string Title { get; set; }
        public int StudentID { get; set; }
        public int TeacherID { get; set; }


        public Subject(int subjectID, string title, int studentID, int teacherID)
        {
            SubjectID = subjectID;
            Title = title;
            StudentID = studentID;
            TeacherID = teacherID;
        }
    }
}
