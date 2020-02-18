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
    public class GameMacroRepository
    {
        private readonly string _connectionString;

        public GameMacroRepository()
        {
            _connectionString = Settings1.Default.ConnectionString;
        }

        public GameMacroModel GetByUID(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM [dbo].[Pangya_Game_Macro] WHERE UID = @UID";

                return connection.QuerySingleOrDefault<GameMacroModel>(query, new { UID = uid });
            }
        }
    }
}
