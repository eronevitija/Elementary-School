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
                            row["Title"].ToString(),
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
                            DataTable dt = new DataTable();
                            sqlDataAdapter.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                return null;
                            }
                            DataRow row = dt.Rows[0];

                            int subjectsID = Convert.ToInt32(row["SubjectID"]);
                            string title = row["Title"] != DBNull.Value ? Convert.ToString(row["Title"]) : string.Empty;
                            int studentID = Convert.ToInt32(row["StudentID"]);
                            int teacherID = Convert.ToInt32(row["TeacherID"]);

                            Subject obj = new Subject(subjectsID, title, studentID,teacherID);
                            return obj;
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
