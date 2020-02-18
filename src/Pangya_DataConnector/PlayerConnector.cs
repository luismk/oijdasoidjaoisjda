using Pangya_DataConnector.DataBase;
using PangyaAPI;
using System.Collections.Generic;
using System.Linq;

namespace Pangya_DataConnector
{
    public class PlayerConnector
    {
        public PangyaEntities _db { get; set; }

        private Player _player { get; set; }

        public Pangya_Member Member { get; set; }

        public Pangya_User_Statistics User_Statistics { get; set; }

        public ProcGuildGetPlayerData_Result GuilData { get; set; }

        public USP_GAME_LOGIN_Result GameLoginData { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public PlayerConnector(Player player)
        {
            _player = player;
            _db = new PangyaEntities();

            Member = _db.Pangya_Member.FirstOrDefault(p => p.Username == _player.Username);

            if (Member != null)
            {
                User_Statistics = _db.Pangya_User_Statistics.FirstOrDefault(p => p.UID == Member.UID);

                var guild = $"Exec [dbo].[ProcGuildGetPlayerData] @UID = '{Member.UID}'";

                GuilData = _db.Database.SqlQuery<ProcGuildGetPlayerData_Result>(guild).FirstOrDefault();
            }
        }

        ///// <summary>
        ///// Verifica se o usuário está Banido
        ///// </summary>
        ///// <returns></returns>
        //public bool IsBan()
        //{
        //    return _db.Database.SqlQuery<int>($"SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Member] WHERE Username = '{_player.Username}' AND IDState > 0").First() == 1;
        //}

        ///// <summary>
        ///// Verifica se o Username existe
        ///// </summary>
        //public bool UserNameIsValid()
        //{
        //    return _db.Database.SqlQuery<int>($"SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Member] WHERE Username = '{_player.Username}'").First() == 1;
        //}

        ///// <summary>
        ///// Verifica se a senha é válida
        ///// </summary>
        //public bool PasswordIsValid(string password)
        //{
        //    return _db.Database.SqlQuery<int>($"SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Member] WHERE Username = '{_player.Username}' AND SUBSTRING(sys.fn_sqlvarbasetostr(HASHBYTES('MD5', Password)), 3, 32) = '{password}'").First() == 1;
        //}

        /// <summary>
        /// Verifica se o Username existe
        /// </summary>
        public bool AccountInUse()
        {
            return _db.Database.SqlQuery<int>($"SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Member] WHERE Username = '{_player.Username}' AND Logon = '1'").First() == 1;
        }

        //public bool IsFirstSet()
        //{
        //    return _db.Database.SqlQuery<int>($"SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Character] WHERE UID = '{Member.UID}'").First() == 0;
        //}

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public Pangya_Member AddPangyaMember(Pangya_Member model)
        {
            Member = _db.Pangya_Member.Add(model);

            return Member;
        }

        #region Procedures

        public int? USP_NICKNAME_CHECK(string nickname)
        {
            return _db.USP_NICKNAME_CHECK(nickname).FirstOrDefault();
        }

        public byte? USP_FIRST_CREATION(int characterTypeId, byte hair_color)
        {
            return _db.USP_FIRST_CREATION(Member.UID, characterTypeId, hair_color, Member.Nickname).FirstOrDefault();
        }

        public List<ProcGetGameServer_Result> ProcGetGameServer()
        {
            return _db.ProcGetGameServer().ToList();
        }

        public List<ProcGetMessengerServer_Result> ProcGetMessengerServer()
        {
            return _db.ProcGetMessengerServer().ToList();
        }

        #endregion

        #region Set

        /// <summary>
        /// Seta novo nickname
        /// </summary>
        public void SetNickName(string nickname)
        {
            Member.Nickname = nickname;
            SaveChanges();
        }
        /// <summary>
        /// Altera para 0
        /// </summary>
        public void SetLogon()
        {
            _db.Database.SqlQuery<PangyaEntities>($"UPDATE [dbo].Pangya_Member SET Logon = 0 WHERE Username = {_player.Username}'");
        }

        public void SetAuthKey_Login(string key)
        {
            _db.Database.SqlQuery<PangyaEntities>($"UPDATE [dbo].Pangya_Member SET AuthKey_Login = {key} WHERE Username = {_player.Username}'");
        }
        #endregion

        #region Get

        /// <summary>
        /// Obtém 
        /// </summary>
        /// <returns></returns>
        public Pangya_Game_Macro GetMacros()
        {
            return _db.Pangya_Game_Macro.FirstOrDefault(p => p.UID == Member.UID);
        }

        public void GetGameLogin()
        {
            var data = $"Exec [dbo].[USP_GAME_LOGIN] @USERID = '{Member.Username}', @UID = '{Member.UID}', @CODE1 = '{Member.AuthKey_Login}', @CODE2 = '{Member.AuthKey_Game}'";

            GameLoginData = _db.Database.SqlQuery<USP_GAME_LOGIN_Result>(data).FirstOrDefault();
        }    

        #endregion
    }


}
