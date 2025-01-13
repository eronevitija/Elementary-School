using System.Data;
using ElementarySchool.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ElementarySchool.Services
{
    public class ClassService(DatabaseConnection dbConnection)
    {

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
                        row["Title"].ToString(),
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
                            DataTable dt = new DataTable();
                            sqlDataAdapter.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                return null;
                            }
                            DataRow row = dt.Rows[0];

                            int classesID = Convert.ToInt32(row["ClassID"]);
                            string title = row["Title"].ToString();
                            int teacherID = Convert.ToInt32(row["TeacherID"]);

                            Class obj = new Class(classesID, title, teacherID);

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
