using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UGPangya.Connector.Properties;
using UGPangya.Connector.Repository.Model;

namespace UGPangya.Connector.Repository
{
    public class UserStatisticsRepository
    {

        private readonly string _connectionString;

        public UserStatisticsRepository()
        {
            _connectionString = Settings1.Default.ConnectionString;
        }

        public UserStatisticsModel GetByUID(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM [dbo].[Pangya_User_Statistics] WHERE UID = @UID";

                return connection.QuerySingleOrDefault<UserStatisticsModel>(query, new { UID = uid });
            }
        }
    }
}
