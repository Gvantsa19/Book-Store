using Microsoft.Data.SqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.DBContexts
{
    public class DbcontextDapper:IDisposable
    {
        private ConcurrentBag<SqlConnection> _connections = new ConcurrentBag<SqlConnection>();
        private readonly string _connectionString;

        public DbcontextDapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            _connections.Add(connection);

            return connection;
        }

        public void Dispose()
        {
            foreach (var item in _connections)
            {
                item.Dispose();
            }
            _connections.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
