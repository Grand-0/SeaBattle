using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlHelperLib
{
    public class SqlHelper
    {
        private string _connectionString { get; }
        public SqlHelper(string sqlConnectionString)
        {
            _connectionString = sqlConnectionString;
        }

        public T GetEntity<T>(string fff, T emptyObj) {
            var t = emptyObj;

            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            {

                foreach (var f in t.GetType().GetProperties())
                {
                    
                }
            }

            return t;
        }
    }
}
