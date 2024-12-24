using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


namespace ElementarySchool.Services
{
    public class AttendanceService(DatabaseConnection dbConnection)
    {
        private readonly DatabaseConnection db = dbConnection;

        #region GetAllStudents
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
        #endregion

    }
}
