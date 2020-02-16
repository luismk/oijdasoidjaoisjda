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
    public class GameMacroRepository
    {
        private readonly string _connectionString;

        public GameMacroRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public GameMacro GetByUID(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM [dbo].[Pangya_Game_Macro] WHERE UID = @UID";

                return connection.QuerySingleOrDefault<GameMacro>(query, new { UID = uid });
            }
        }
    }
}
