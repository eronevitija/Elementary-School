using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace ElementarySchool.Services
{
    public class DatabaseConnection
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public DatabaseConnection(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("ElementarySchoolContext") ?? string.Empty;
        }

        public SqlConnection GetSqlConnection()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);
                return sqlConn;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to create database connection" + e);
            }
        }


        public DataTable ExecStoredProcedure(string storedProcedureName)
        {
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
                            DataTable dt = new DataTable();
                            sqlDataAdapter.Fill(dt);
                            return dt;
                        }
                    }
                }
                catch (Exception e)
                {

                    throw new Exception("Error executing stored procedure" + e);
                }
            }
        }



    }
}
