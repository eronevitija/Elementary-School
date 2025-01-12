using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ElementarySchool.Services
{
    public class TeacherService
    {
        private readonly string connString;
        private readonly DatabaseConnection dbConnection;

        public TeacherService(IConfiguration config, DatabaseConnection dbConn)
        {
            connString = config.GetConnectionString("ElementarySchoolContext")
                ?? throw new ArgumentException("Connection string 'Elementary School Context' not found.");
            dbConnection = dbConn ?? throw new ArgumentException(nameof(dbConn));
        }

        public string GetConnectionString()
        {
            return connString;
        }

        public List<Teacher> GetAllTeachers()
        {
            try
            {
                List<Teacher> teacher = new List<Teacher>();
                DataTable dt = new DataTable();

                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("usp_ShowTeacherList", sqlConn);
                    sqlDataAdapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        Teacher t = new Teacher
                        (
                            teacherID: Convert.ToInt32(row["TeacherID"]),
                            firstName: row["FirstName"]?.ToString() ?? string.Empty,
                            lastName:row["LastName"]?.ToString() ?? string.Empty,
                            email: row["Email"]?.ToString() ?? string.Empty,
                            phoneNo: row["PhoneNo"]?.ToString() ?? string.Empty,
                            subject: row["Subject"]?.ToString() ?? string.Empty,
                            address: row["Address"]?.ToString() ?? string.Empty,
                            dateOfHire: row["DateOfHire"] == DBNull.Value ? null : DateTime.Parse(row["DateOfHire"].ToString()),
                            isActive: row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"])
                        );
                        teacher.Add(t);

                    }
                    return teacher;
                }
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

        public void EditTeacher(Teacher t)
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

        public Teacher GetTeacherByID(int stID)
        {
            DataSet ds;
            Teacher t;
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_GetTeacherByID", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ID", stID);
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            DataTable dt = new DataTable();
                            sqlDataAdapter.Fill(dt);

                            ds = new DataSet();
                            sqlDataAdapter.Fill(ds);
                            string teacherIDValue = Convert.ToString((ds.Tables[0].Rows[0]["StudentID"])) as string ?? string.Empty;
                            string firstName = Convert.ToString((ds.Tables[0].Rows[0]["FirstName"])) as string ?? string.Empty;
                            string lastName = Convert.ToString((ds.Tables[0].Rows[0]["LastName"])) as string ?? string.Empty;
                            string email = Convert.ToString((ds.Tables[0].Rows[0]["Email"])) as string ?? string.Empty;
                            string phoneNo = Convert.ToString((ds.Tables[0].Rows[0]["PhoneNo"])) as string ?? string.Empty;
                            string subject = Convert.ToString((ds.Tables[0].Rows[0]["PhoneNo"])) as string ?? string.Empty;
                            string address = Convert.ToString((ds.Tables[0].Rows[0]["Address"])) as string ?? string.Empty;
                            string dateOfHire = Convert.ToString((ds.Tables[0].Rows[0]["EnrollmentDate"])) as string ?? string.Empty;
                            string isActive = Convert.ToString((ds.Tables[0].Rows[0]["IsActive"])) as string ?? string.Empty;

                            t = new Teacher(Int32.Parse(teacherIDValue), firstName, 
                                lastName, email, phoneNo, subject, address,DateTime.Parse(dateOfHire),
                                Boolean.Parse(isActive));

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return t;
        }

    }
}
