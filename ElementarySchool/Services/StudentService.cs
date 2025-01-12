using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ElementarySchool.Data;


namespace ElementarySchool.Services
{                           
    public class StudentService
    {
        private readonly string connString;
        private readonly DatabaseConnection dbConnection;

        public StudentService(IConfiguration config, DatabaseConnection dbConn)
        {
            connString = config.GetConnectionString("ElementarySchoolContext")
                ?? throw new ArgumentException("Connection string 'Elementary School Context' not found.");
            dbConnection = dbConn ?? throw new ArgumentException(nameof(dbConn));
        }

        public string GetConnectionString() 
        {
            return connString;
        }
        public List<Student> GetAllStudents()
        {
            try
            {
                List<Student> students = new List<Student>();
                DataTable dt = new DataTable();

                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("usp_ShowStudentList",sqlConn);
                    sqlDataAdapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                       
                        Student student = new Student
                        (
                            studentID: Convert.ToInt32(row["StudentID"]),
                            firstName: row["FirstName"]?.ToString() ?? string.Empty,
                            fatherName: row["FatherName"]?.ToString() ?? string.Empty,
                            lastName: row["LastName"]?.ToString() ?? string.Empty,
                            gender: row["Gender"]?.ToString() ?? string.Empty,
                            birthdate: row["Birthdate"] == DBNull.Value ? null : DateTime.Parse(row["Birthdate"].ToString()),
                            address: row["Address"]?.ToString() ?? string.Empty,
                            phoneNo: row["PhoneNo"]?.ToString() ?? string.Empty,
                            email: row["Email"]?.ToString() ?? string.Empty,
                            enrollmentDate: row["Birthdate"] == DBNull.Value ? null : DateTime.Parse(row["Birthdate"].ToString()),
                            isActive: row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"])
                        );
                        students.Add(student);
                    }
                    }
                return students;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertStudent(Student student)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_InsertStudent", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;   
                        sqlCmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                        sqlCmd.Parameters.AddWithValue("@FatherName", student.FatherName);
                        sqlCmd.Parameters.AddWithValue("@LastName", student.LastName);
                        sqlCmd.Parameters.AddWithValue("@Gender", student.Gender);
                        sqlCmd.Parameters.AddWithValue("@Birthdate", student.Birthdate);
                        sqlCmd.Parameters.AddWithValue("@Address", student.Address);
                        sqlCmd.Parameters.AddWithValue("@PhoneNo", student.PhoneNo);
                        sqlCmd.Parameters.AddWithValue("@Email", student.Email);
                        sqlCmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);
                        sqlCmd.Parameters.AddWithValue("@IsActive", student.IsActive);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditStudent(Student st)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_EditStudent", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@StudentID", st.StudentID);
                        sqlCmd.Parameters.AddWithValue("@FirstName", st.FirstName);
                        sqlCmd.Parameters.AddWithValue("@FatherName", st.FatherName);
                        sqlCmd.Parameters.AddWithValue("@LastName", st.LastName);
                        sqlCmd.Parameters.AddWithValue("@Gender", st.Gender);
                        sqlCmd.Parameters.AddWithValue("@Birthdate", st.Birthdate);
                        sqlCmd.Parameters.AddWithValue("@Address", st.Address);
                        sqlCmd.Parameters.AddWithValue("@PhoneNo", st.PhoneNo);
                        sqlCmd.Parameters.AddWithValue("@Email", st.Email);
                        sqlCmd.Parameters.AddWithValue("@EnrollmentDate", st.EnrollmentDate);
                        sqlCmd.Parameters.AddWithValue("@IsActive", st.IsActive);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }
        }

        public void DeleteStudent(int? studentID)
        {
            try
            {
                if (studentID != null || studentID > 0)
                {
                    using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                    {
                        sqlConn.Open();
                        using (SqlCommand sqlCmd = new SqlCommand("usp_DeleteStudent",sqlConn))
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@StudentID", studentID);
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

        public Student GetStudentByID(int stID)
        {
            DataSet ds;
            Student st;
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_GetStudentByID", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ID", stID);
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            DataTable dt = new DataTable();
                            sqlDataAdapter.Fill(dt);

                            ds = new DataSet();
                            sqlDataAdapter.Fill(ds);
                            string studentIDValue = Convert.ToString((ds.Tables[0].Rows[0]["StudentID"])) as string ?? string.Empty;
                            string firstName = Convert.ToString((ds.Tables[0].Rows[0]["FirstName"])) as string ?? string.Empty;
                            string fatherName = Convert.ToString((ds.Tables[0].Rows[0]["FatherName"])) as string ?? string.Empty;
                            string lastName = Convert.ToString((ds.Tables[0].Rows[0]["LastName"])) as string ?? string.Empty;
                            string gender = Convert.ToString((ds.Tables[0].Rows[0]["Gender"])) as string ?? string.Empty;
                            string birthdate = Convert.ToString((ds.Tables[0].Rows[0]["Birthdate"])) as string ?? string.Empty;
                            string address = Convert.ToString((ds.Tables[0].Rows[0]["Address"])) as string ?? string.Empty;
                            string phoneNo = Convert.ToString((ds.Tables[0].Rows[0]["PhoneNo"])) as string ?? string.Empty;
                            string email = Convert.ToString((ds.Tables[0].Rows[0]["Email"])) as string ?? string.Empty;
                            string enrollmentDate = Convert.ToString((ds.Tables[0].Rows[0]["EnrollmentDate"])) as string ?? string.Empty;
                            string isActive = Convert.ToString((ds.Tables[0].Rows[0]["IsActive"])) as string ?? string.Empty;

                            st = new Student(Int32.Parse(studentIDValue), firstName,fatherName,
                                lastName,gender,DateTime.Parse(birthdate),address, phoneNo,email,DateTime.Parse(enrollmentDate),
                                Boolean.Parse(isActive));

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return st;
        }

    }
}
