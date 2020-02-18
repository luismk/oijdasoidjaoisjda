using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using UGPangya.API.Repository.Models;

namespace UGPangya.API.Repository
{
    public class CaddieRepository
    {
        private readonly string _connectionString;

        public CaddieRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public IEnumerable<Caddie> GetByUID(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<Caddie>("ProcGetCaddies", new {UID = uid},
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}