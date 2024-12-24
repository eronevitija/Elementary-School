namespace ElementarySchool.Models
{
    public class Grades
    {
        public int GradeID { get; set; }
        public int SubjectID { get; set; }
        public int StudentID { get; set; }
        public char Grade { get; set; }

        public Grades(int gradeID, int subjectID, int studentID, char grade)
        {
            GradeID = gradeID;
            SubjectID = subjectID;
            StudentID = studentID;
            Grade = grade;
        }

    }
}
