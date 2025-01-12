using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


namespace ElementarySchool.Services
{
    public class AttendanceService
    {
        private readonly DatabaseConnection dbConnection;

        public AttendanceService(DatabaseConnection dbConn)
        {
            dbConnection = dbConn;
        }

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
                        row["FirstName"]?.ToString() ?? string.Empty,
                        row["FatherName"]?.ToString() ?? string.Empty,
                        row["LastName"]?.ToString() ?? string.Empty,
                        row["Gender"]?.ToString() ?? string.Empty,
                        (DateTime)row["Birthdate"],
                        row["Address"]?.ToString() ?? string.Empty,
                        row["PhoneNo"]?.ToString() ?? string.Empty,
                        row["Email"]?.ToString() ?? string.Empty,
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



    }
}
