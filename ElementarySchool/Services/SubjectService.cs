using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
namespace ElementarySchool.Services
{
    public class SubjectService(DatabaseConnection dbConn)
    {
            private readonly DatabaseConnection dbConnection = dbConn;

            public List<Subject> GetAllSubjects()
            {
                try
                {
                    List<Subject> subjects = new List<Subject>();

                    DataTable dt = dbConnection.ExecStoredProcedure("usp_ShowSubjectList");
                    foreach (DataRow row in dt.Rows)
                    {
                        Subject subject = new Subject
                        (
                            (int)row["SubjectID"],
                            row["Title"]?.ToString() ?? string.Empty,
                            (int)row["StudentID"],
                            (int)row["TeacherID"]
                        );
                        subjects.Add(subject);
                    }
                    return subjects;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        public void InsertSubject(Subject subjects)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_InsertSubject", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@Title", subjects.Title);
                        sqlCmd.Parameters.AddWithValue("@StudentID", subjects.StudentID);
                        sqlCmd.Parameters.AddWithValue("@TeacherID", subjects.TeacherID);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditSubject(Subject subjects)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_EditSubject", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@SubjectID", subjects.SubjectID);
                        sqlCmd.Parameters.AddWithValue("@Title", subjects.Title);
                        sqlCmd.Parameters.AddWithValue("@StudentID", subjects.StudentID);
                        sqlCmd.Parameters.AddWithValue("@TeacherID", subjects.TeacherID);

                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void DeleteStudent(int? subjectID)
        {
            try
            {
                if (subjectID!= null || subjectID > 0)
                {
                    using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                    {
                        sqlConn.Open();
                        using (SqlCommand sqlCmd = new SqlCommand("usp_DeleteSubject", sqlConn))
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@SubjectID",subjectID);
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


        public Subject GetSubjectByID(int subjectID)
        {
            DataSet ds;
            Subject subject;
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_GetSubjectByID", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ID", subjectID);
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            ds = new DataSet();
                            sqlDataAdapter.Fill(ds);
                            string subjectIDValue = Convert.ToString((ds.Tables[0].Rows[0]["SubjectID"])) as string ?? string.Empty;
                            string title = Convert.ToString((ds.Tables[0].Rows[0]["Title"])) as string ?? string.Empty;
                            string studentID = Convert.ToString((ds.Tables[0].Rows[0]["StudentID"])) as string ?? string.Empty;
                            string teacherID = Convert.ToString((ds.Tables[0].Rows[0]["TeacherID"])) as string ?? string.Empty;

                            subject = new Subject(Int32.Parse(subjectIDValue), title, Int32.Parse(studentID),Int32.Parse(teacherID));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return subject;
        }


    }
}
