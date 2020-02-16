using Dapper;
using PangyaAPI.Repository.DapperExt;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository
{
    public class MemberRepository
    {
        private readonly string _connectionString;

        public MemberRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public Member GetByUserName(string userName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM [dbo].[Pangya_Member] WHERE Username = @UserName";

                return connection.QuerySingleOrDefault<Member>(query, new { UserName = userName });
            }
        }

        public bool PasswordMD5IsValid(string userName, string passwordMD5)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Member] WHERE Username = @UserName AND SUBSTRING(sys.fn_sqlvarbasetostr(HASHBYTES('MD5', Password)), 3, 32) = @Password";

                return connection.QuerySingle<int>(query, new { UserName = userName, Password = passwordMD5.ToLower() }) > 0;
            }
        }

        public bool Update(Member model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE [dbo].[Pangya_Member] SET 
                [AuthKey_Login] = @AuthKey_Login, 
                [AuthKey_Game] = @AuthKey_Game, 
                [IPAddress] = @IPAddress, 
                [Nickname] = @Nickname 
                WHERE UID = @UID";

                var result = connection.Execute(query, model);

                return result == 1;
            }
        }

        public int USP_GAME_LOGIN(int uid, string userName, string authKeyLogin, string authKeyGame)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return (int)connection.QuerySingleOrDefault<dynamic>("USP_GAME_LOGIN", new { UID = uid, USERID = userName, Code1 = authKeyLogin, Code2 = authKeyGame },
                    commandType: CommandType.StoredProcedure).Code;
            }
        }

    }
}
