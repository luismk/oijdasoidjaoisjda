using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using UGPangya.API.Repository.Models;

namespace UGPangya.API.Repository
{
    public class PangyaServerRepository
    {
        private readonly string _connectionString;


        public PangyaServerRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public IEnumerable<PangyaServer> GetGameServers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT * FROM [dbo].[Pangya_Server] WHERE ServerType = 0";

                return connection.Query<PangyaServer>(query);
            }
        }

        public IEnumerable<PangyaServer> GetMessengerServers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT * FROM [dbo].[Pangya_Server] WHERE ServerType = 3";

                return connection.Query<PangyaServer>(query);
            }
        }
    }
}