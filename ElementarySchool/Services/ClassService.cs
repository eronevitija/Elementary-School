using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ElementarySchool.Services
{
    public class ClassService
    {
        private DatabaseConnection dbConnection;

        public ClassService(DatabaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public List<Class> GetAllClasses()
        {
            try
            {
                List<Class> c = new List<Class>();

                DataTable dt = dbConnection.ExecStoredProcedure("usp_ShowClassList");
                foreach (DataRow row in dt.Rows)
                {
                    Class classObj = new Class
                    (
                        (int)row["ClassID"],
                        row["Title"]?.ToString() ?? string.Empty,
                        (int)row["TeacherID"]
                    );
                    c.Add(classObj);
                }
                return c;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void InsertClass(Class c)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_InsertClass", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@Title", c.Title);
                        sqlCmd.Parameters.AddWithValue("@TeacherID", c.TeacherID);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void EditClass(Class c)
        {
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_EditClass", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ClassID", c.ClassID);
                        sqlCmd.Parameters.AddWithValue("@Title", c.Title);
                        sqlCmd.Parameters.AddWithValue("@TeacherID", c.TeacherID);
                        
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void DeleteClass(int? classID)
        {
            try
            {
                if (classID != null || classID > 0)
                {
                    using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                    {
                        sqlConn.Open();
                        using (SqlCommand sqlCmd = new SqlCommand("usp_DeleteClass", sqlConn))
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@ClassID", classID);
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

        public Class GetClassByID(int classID)
        {
            DataSet ds;
            Class obj;
            try
            {
                using (SqlConnection sqlConn = dbConnection.GetSqlConnection())
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("usp_GetClassByID", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ID", classID);
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            ds = new DataSet(); 
                            sqlDataAdapter.Fill(ds);
                            string classIDValue = Convert.ToString((ds.Tables[0].Rows[0]["ClassID"])) as string ?? string.Empty;
                            string title = Convert.ToString((ds.Tables[0].Rows[0]["Title"])) as string ?? string.Empty;
                            string teacherID = Convert.ToString((ds.Tables[0].Rows[0]["TeacherID"])) as string ?? string.Empty;

                             obj = new Class(Int32.Parse(classIDValue),title,Int32.Parse(teacherID));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return obj;
        }


    }
}
