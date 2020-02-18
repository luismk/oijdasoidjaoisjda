using System.Data.SqlClient;
using Dapper;
using UGPangya.Connector.Properties;
using UGPangya.Connector.Repository.Model;

namespace UGPangya.Connector.Repository
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

                return connection.QuerySingleOrDefault<GameMacroModel>(query, new {UID = uid});
            }
        }
    }
}