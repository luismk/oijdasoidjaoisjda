using Dapper;
using PangyaConnector.Properties;
using PangyaConnector.Repository.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaConnector.Repository
{
    public class PangyaServerRepository
    {
        private readonly string _connectionString;


        public PangyaServerRepository()
        {
            _connectionString = Settings1.Default.ConnectionString;
        }

        public IEnumerable<PangyaServerModel> GetGameServers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT * FROM [dbo].[Pangya_Server] WHERE ServerType = 0";

                return connection.Query<PangyaServerModel>(query);
            }
        }

        public IEnumerable<PangyaServerModel> GetMessengerServers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT * FROM [dbo].[Pangya_Server] WHERE ServerType = 3";

                return connection.Query<PangyaServerModel>(query);
            }
        }
    }
}
