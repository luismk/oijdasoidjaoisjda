using System.Data.SqlClient;
using Dapper;
using UGPangya.API.Repository.Models;

namespace UGPangya.API.Repository
{
    public class UserStatisticsRepository
    {
        private readonly string _connectionString;

        public UserStatisticsRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public UserStatistics GetByUID(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM [dbo].[Pangya_User_Statistics] WHERE UID = @UID";

                return connection.QuerySingleOrDefault<UserStatistics>(query, new {UID = uid});
            }
        }
    }
}