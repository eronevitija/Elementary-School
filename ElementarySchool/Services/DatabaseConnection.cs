using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace ElementarySchool.Services
{
    public class DatabaseConnection(string connString)
    {
        private readonly string connectionString = connString;


        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }


        public DataTable ExecStoredProcedure(string storedProcedureName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlConn = GetSqlConnection())
            {
                try
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand(storedProcedureName,sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlDataAdapter.Fill(dt);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return dt;
        }



    }
}
