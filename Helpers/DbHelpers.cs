using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Drugstores
{
    internal class DbHelpers
    {
        static string _connectionString;
        static public string СonnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                return _connectionString;
            }
        }

        static public bool Exists(string tableName, int id)
        {
            using (SqlConnection connection = new SqlConnection(СonnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT 1 FROM {tableName} WHERE Id = {id}", connection);
                SqlParameter nameParam = new SqlParameter("@id", id);
                command.Parameters.Add(nameParam);
                return command.ExecuteScalar() != null;
            }
        }
    }
}
