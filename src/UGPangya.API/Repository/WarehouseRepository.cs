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
    public class WarehouseRepository
    {
        private readonly string _connectionString;

        public WarehouseRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public IEnumerable<Warehouse> GetByUID(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<Warehouse>("ProcGetItemWarehouse", new { UID = uid },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
