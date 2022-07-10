using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelperLib.Entities
{
    class Procedure
    {
        private SqlConnection _connection { get; }
        private Procedure() { }

        private Procedure(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }


    }
}
