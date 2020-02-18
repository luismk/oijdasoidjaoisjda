using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UGPangya.API.Repository.Models;

namespace UGPangya.API.Repository
{
    public class MascotRepository
    {
        private readonly string _connectionString;

        public MascotRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public IEnumerable<Mascot> GetByUID(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<Mascot>("ProcGetMascot", new { UID = uid },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
