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

                return connection.QuerySingleOrDefault<UserStatistics>(query, new { UID = uid });
            }
        }
    }
}
