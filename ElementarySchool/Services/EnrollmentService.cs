using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ElementarySchool.Services
{
    public class EnrollmentService(DatabaseConnection dbConn)
    {
        private readonly DatabaseConnection dbConnection = dbConn;

        public List<Enrollment> GetAllEnrollments()
        {
            try
            {
                List<Enrollment> enrollmentList = new List<Enrollment>();

                DataTable dt = dbConnection.ExecStoredProcedure("usp_ShowEnrollmentList");
                foreach (DataRow row in dt.Rows)
                {
                    Enrollment enrollment = new Enrollment
                    (
                        (int)row["EnrollmentID"],
                        (DateTime)row["EnrollmentDate"],
                        (int)row["StudentID"],
                        (int)row["ClassID"]
                    );
                    enrollmentList.Add(enrollment);

                }
                return enrollmentList;
            }
            catch (Exception)
            {
                throw;
            }
        }






        public void InsertEnrollment(Enrollment  en)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_AddEnrollment", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@EnrollmentDate", en.EnrollmentDate);
                        sqlCmd.Parameters.AddWithValue("@StudentID", en.StudentID);
                        sqlCmd.Parameters.AddWithValue("@ClassID", en.ClassID);

                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditEnrollment(Enrollment en)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_EditEnrollment", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@EnrollmentID", en.StudentID);
                        sqlCmd.Parameters.AddWithValue("@EnrollmentDate", en.EnrollmentDate);
                        sqlCmd.Parameters.AddWithValue("@StudentID", en.StudentID);
                        sqlCmd.Parameters.AddWithValue("@ClassID", en.ClassID);

                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void DeleteEnrollment(int? enrollmentID)
        {
            try
            {
                if (enrollmentID != null || enrollmentID > 0)
                {
                    using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                    {
                        sqlConn.Open();
                        using (SqlCommand sqlCmd = new SqlCommand("usp_DeleteEnrollment", sqlConn))
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@EnrollmentID", enrollmentID);
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


        public Enrollment GetEnrollment(int enID)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_GetEnrollmentByID", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ID", enID);
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            DataTable dt = new DataTable();
                            sqlDataAdapter.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                return null;
                            }
                            DataRow row = dt.Rows[0];

                            int enrollmentID = Convert.ToInt32(row["EnrollmentID"]);
                            DateTime enrollmentDate = Convert.ToDateTime(row["EnrollmentDate"]);
                            int studentID = Convert.ToInt32(row["StudentID"]);
                            int classID = Convert.ToInt32(row["ClassID"]);

                            Enrollment en = new Enrollment(enrollmentID, Convert.ToDateTime(enrollmentDate), studentID, classID);
                            return en;
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
