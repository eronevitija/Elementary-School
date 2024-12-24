using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ElementarySchool.Services
{
    public class TeacherService(DatabaseConnection dbConn)
    {
        private readonly DatabaseConnection dbConnection = dbConn;

        public List<Teacher> GetAllTeachers()
        {
            try
            {
                List<Teacher> teacher = new List<Teacher>();

                DataTable dt = dbConnection.ExecStoredProcedure("usp_ShowTeacherList");
                foreach (DataRow row in dt.Rows)
                {
                    Teacher t = new Teacher
                    (
                        (int)row["TeacherID"],
                        row["FirstName"].ToString(),
                        row["LastName"].ToString(),
                        row["Email"].ToString(),
                        row["PhoneNo"].ToString(),
                        row["Subject"].ToString(),
                        row["Address"].ToString(),
                        (DateTime)row["DateOfHire"],
                        (bool)row["IsActive"]
                    );
                    teacher.Add(t);

                }
                return teacher;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertTeacher(Teacher t)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_InsertTeacher", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@FirstName", t.FirstName);
                        sqlCmd.Parameters.AddWithValue("@LastName", t.LastName);
                        sqlCmd.Parameters.AddWithValue("@Email", t.Email);
                        sqlCmd.Parameters.AddWithValue("@PhoneNo", t.PhoneNo);
                        sqlCmd.Parameters.AddWithValue("@Subject", t.Subject);
                        sqlCmd.Parameters.AddWithValue("@Address", t.Address);
                        sqlCmd.Parameters.AddWithValue("@DateOfHire", t.DateOfHire);
                        sqlCmd.Parameters.AddWithValue("@IsActive", t.IsActive);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditStudent(Teacher t)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_EditTeacher", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@TeacherID", t.TeacherID);
                        sqlCmd.Parameters.AddWithValue("@FirstName", t.FirstName);
                        sqlCmd.Parameters.AddWithValue("@LastName", t.LastName);
                        sqlCmd.Parameters.AddWithValue("@Email", t.Email);
                        sqlCmd.Parameters.AddWithValue("@PhoneNo", t.PhoneNo);
                        sqlCmd.Parameters.AddWithValue("@PhoneNo", t.Subject);
                        sqlCmd.Parameters.AddWithValue("@Address", t.Address);
                        sqlCmd.Parameters.AddWithValue("@DateOfHire", t.DateOfHire);
                        sqlCmd.Parameters.AddWithValue("@IsActive", t.IsActive);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteTeacher(int? teacherID)
        {
            try
            {
                if (teacherID != null || teacherID > 0)
                {
                    using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                    {
                        sqlConn.Open();
                        using (SqlCommand sqlCmd = new SqlCommand("usp_DeleteTeacher", sqlConn))
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@TeacherID", teacherID);
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


    }
}
