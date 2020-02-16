using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository
{
    public class LoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public int? USP_NICKNAME_CHECK(string nickName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QuerySingleOrDefault<int?>("USP_NICKNAME_CHECK", new { NICKNAME = nickName },
                    commandType: CommandType.StoredProcedure);
            }
        }
        public byte? USP_FIRST_CREATION(int uid, int char_typeId, int hairColor, string nickName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.QuerySingleOrDefault<byte?>("USP_FIRST_CREATION",
                    new { UID = uid, CHAR_TYPEID = char_typeId, HAIRCOLOUR = hairColor, NICKNAME = nickName },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
