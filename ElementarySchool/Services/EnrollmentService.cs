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


        public Enrollment GetEnrollmentById(int enID)
        {
            DataSet ds;
            Enrollment enrollment;

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
                            ds = new DataSet();
                            sqlDataAdapter.Fill(ds);
                            string enrollmentIDValue = Convert.ToString((ds.Tables[0].Rows[0]["EnrollmentID"])) as string ?? string.Empty;
                            string enrollmentDate = Convert.ToString((ds.Tables[0].Rows[0]["EnrollmentDate"])) as string ?? string.Empty;
                            string studentID = Convert.ToString((ds.Tables[0].Rows[0]["StudentID"])) as string ?? string.Empty;
                            string classID = Convert.ToString((ds.Tables[0].Rows[0]["ClassID"])) as string ?? string.Empty;

                            enrollment = new Enrollment(Int32.Parse(enrollmentIDValue), DateTime.Parse(enrollmentDate),
                                Int32.Parse(studentID), Int32.Parse(classID));
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return enrollment;
        }

    }
}
