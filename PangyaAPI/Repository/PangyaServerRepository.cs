using Dapper;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository
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
