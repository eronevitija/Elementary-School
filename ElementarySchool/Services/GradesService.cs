using ElementarySchool.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ElementarySchool.Services
{
    public class GradesService(DatabaseConnection dbConn)
    {
        private readonly DatabaseConnection dbConnection = dbConn;

        public List<Grades> GetAllGrades()
        {
            try
            {
                List<Grades> gradesList = new List<Grades>();

                DataTable dt = dbConnection.ExecStoredProcedure("usp_ShowGradesList");
                foreach (DataRow row in dt.Rows)
                {
                    Grades grade = new Grades
                    (
                        (int)row["GradeID"],
                        (int)row["SubjectID"],
                        (int)row["StudentID"],
                        (char)row["Grade"]
                    );
                    gradesList.Add(grade);

                }
                return gradesList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertGrade(Grades grade)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_InsertGrade", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@SubjectID", grade.SubjectID);
                        sqlCmd.Parameters.AddWithValue("@StudentID", grade.StudentID);
                        sqlCmd.Parameters.AddWithValue("@Grade", grade.Grade);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditGrade(Grades grade)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_EditGrade", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@GradeID", grade.GradeID);
                        sqlCmd.Parameters.AddWithValue("@SubjectID", grade.SubjectID);
                        sqlCmd.Parameters.AddWithValue("@StudentID", grade.StudentID);
                        sqlCmd.Parameters.AddWithValue("@Grade", grade.Grade);
                     
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void DeleteGrade(int? gradeID)
        {
            try
            {
                if (gradeID != null || gradeID > 0)
                {
                    using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                    {
                        sqlConn.Open();
                        using (SqlCommand sqlCmd = new SqlCommand("usp_DeleteGrade", sqlConn))
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@GradeID", gradeID);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Grades GetGradeByID(int gradeID)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_GetStudentByID", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ID", gradeID);
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            DataTable dt = new DataTable();
                            sqlDataAdapter.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                return null;
                            }
                            DataRow row = dt.Rows[0];

                            int gradesID = Convert.ToInt32(row["GradeID"]);
                            int subjectID = Convert.ToInt32(row["SubjectID"]);
                            int studentID = Convert.ToInt32(row["StudentID"]);
                            char gradeChar = Convert.ToChar(row["Grade"]);

                            Grades grade = new Grades(gradesID, subjectID, studentID, gradeChar);

                            return grade;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
