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
    public class MemberRepository
    {
        private readonly string _connectionString;

        public MemberRepository()
        {
            _connectionString = Settings1.Default.ConnectionString;
        }


        public bool UserIsValid(string userName, string passwordMD5)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Member] WHERE Username = @UserName AND SUBSTRING(sys.fn_sqlvarbasetostr(HASHBYTES('MD5', Password)), 3, 32) = @Password";

                return connection.QuerySingle<int>(query, new { UserName = userName, Password = passwordMD5.ToLower() }) > 0;
            }
        }

        public bool UserIsBan(string userName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = $"SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Member] WHERE Username = @userName AND IDState > 0";

                return connection.QuerySingle<int>(query, new { userName }) == 1;
            }
        }

        public MemberModel GetByUserName(string userName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM [dbo].[Pangya_Member] WHERE Username = @UserName";

                return connection.Query<MemberModel>(query, new { UserName = userName }).FirstOrDefault();
            }
        }

        public MemberModel GetByUID(int UID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM [dbo].[Pangya_Member] WHERE UID = @UID";

                return connection.Query<MemberModel>(query, new { UID }).FirstOrDefault();
            }
        }

        public bool IsFirstSet(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = $"SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Character] WHERE UID = @uid";

                return connection.QuerySingle<int>(query, new { uid }) == 0;
            }
        }


        public bool UpdateAuthKey(int uid, string authKey_login, string authKey_game)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "UPDATE [dbo].[Pangya_Member] SET [AuthKey_Login] = @authKey_login, [AuthKey_Game] = @authKey_Game WHERE UID = @uid";

                    var result = connection.Execute(query, new { uid, authKey_login, authKey_game });

                    return result == 1;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateNickName(int uid, string nickname)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "UPDATE [dbo].[Pangya_Member] SET [Nickname] = @nickname WHERE UID = @uid";

                    var result = connection.Execute(query, new { uid, nickname });

                    return result == 1;
                }
            }
            catch
            {
                return false;
            }
        }



    }
}
