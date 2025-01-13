using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


namespace ElementarySchool.Services
{                           //this is primary constructor, which is used in C# 9 or higher.
    public class StudentService(DatabaseConnection dbConn)
    {
       private readonly DatabaseConnection dbConnection = dbConn;

        public List<Student> GetAllStudents()
        {
            try
            {
                List<Student> st = new List<Student>();

                DataTable dt = dbConnection.ExecStoredProcedure("usp_ShowStudentList");
                foreach (DataRow row in dt.Rows)
                {
                    Student student = new Student
                    (
                        (int)row["StudentID"],
                        row["FirstName"].ToString(),
                        row["FatherName"].ToString(),
                        row["LastName"].ToString(),
                        row["Gender"].ToString(),
                        (DateTime)row["Birthdate"],
                        row["Address"].ToString(),
                        row["PhoneNo"].ToString(),
                        row["Email"].ToString(),
                        (DateTime)row["EnrollmentDate"],
                        (bool)row["IsActive"]
                    );
                    st.Add(student);

                }
                return st;
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

                            if (dt.Rows.Count == 0)
                            {
                                return null;
                            }
                            DataRow row = dt.Rows[0];
                            
                            int studentID = Convert.ToInt32(row["StudentID"]);
                            string firstName = row["FirstName"] != DBNull.Value ? Convert.ToString(row["FirstName"]) : string.Empty;
                            string fatherName = row["FatherName"] != DBNull.Value ? Convert.ToString(row["FatherName"]) : string.Empty;
                            string lastName = row["LastName"] != DBNull.Value ? Convert.ToString(row["LastName"]) : string.Empty;
                            string gender = row["Gender"] != DBNull.Value ? Convert.ToString(row["Gender"]) : string.Empty;
                            DateTime birthdate = row["Birthdate"] != DBNull.Value ? Convert.ToDateTime(row["FirstName"]) : DateTime.MinValue;
                            string address = row["Address"] != DBNull.Value ? Convert.ToString(row["FirstName"]) : string.Empty;
                            string phoneNo = row["PhoneNo"] != DBNull.Value ? Convert.ToString(row["FirstName"]) : string.Empty;
                            string email = row["Email"] != DBNull.Value ? Convert.ToString(row["FirstName"]) : string.Empty;
                            DateTime enrollmentDate = row["EnrollmentDate"] != DBNull.Value ? Convert.ToDateTime(row["FirstName"]) : DateTime.MinValue;
                            bool isActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["FirstName"]) : false;
                            Student st = new Student(studentID, firstName, fatherName, lastName,
                                gender, Convert.ToDateTime(birthdate), address, phoneNo, email, enrollmentDate, isActive);

                            return st;
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
