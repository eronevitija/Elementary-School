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
            DataSet ds;
            Grades grade;
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
                            ds = new DataSet();
                            sqlDataAdapter.Fill(ds);
                            string gradeIDValue = Convert.ToString((ds.Tables[0].Rows[0]["GradeID"])) as string ?? string.Empty;
                            string subjectID = Convert.ToString((ds.Tables[0].Rows[0]["SubjectID"])) as string ?? string.Empty;
                            string studentID = Convert.ToString((ds.Tables[0].Rows[0]["StudentID"])) as string ?? string.Empty;
                            string studentGrade = Convert.ToString((ds.Tables[0].Rows[0]["Grade"])) as string ?? string.Empty;

                            grade = new Grades(Int32.Parse(gradeIDValue), Int32.Parse(subjectID), 
                                Int32.Parse(studentID), Char.Parse(studentGrade));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return grade;
        }
    }
}
