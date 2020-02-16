using Dapper;
using PangyaAPI.Collections;
using PangyaAPI.Repository.Models;
using PangyaConnector.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public partial class Player
    {
        #region Public Repository Methods

        /// <summary>
        /// Carrega dados do perfil do usuário
        /// </summary>
        /// <returns>Os dados MemberInfo foi carregado</returns>
        public void LoadMember(string userName)
        {
            //Member = new MemberRepository().GetByUserName(userName);

            Member_Old = _pangyaMemberRepository.GetByUserName(userName);

            Guild = _pangyaGuildRepository.GetById(Member_Old.UID);

            User_Statistics = _pangyaUserStatisticsRepository.GetByUID(Member_Old.UID);

            Characters = new CharacterCollection(this);

            UserEquip = _pangyaUserEquipRepository.GetByUID(Member_Old.UID);

            //LoadCaddies
            Caddies = new CaddieCollection(this);

            Mascots = new MascotCollection(this);

            WarehouseCollection = new WarehouseCollection(this);

            //return Member_Old != null;
        }

        public bool LoadUserStatistics()
        {
            User_Statistics = _pangyaUserStatisticsRepository.GetByUID(Member_Old.UID);
            return User_Statistics != null;
        }

        /// <summary>
        /// Verifica se a senha MD5 é a mesma do usuário
        /// </summary>
        public bool PasswordMD5IsValid(string password)
        {
            return _pangyaMemberRepository.PasswordMD5IsValid(Member_Old.Username, password);
        }

        public bool IsBan()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = $"SELECT COUNT(1) FROM[Pangya].[dbo].[Pangya_Member] WHERE Username = @UserName AND IDState > 0";

                return connection.QuerySingle<int>(query, new { Member_Old.Username }) == 1;
            }
        }

        public bool IsFirstSet()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = $"SELECT COUNT(1) FROM [Pangya].[dbo].[Pangya_Character] WHERE UID = @UID";

                return connection.QuerySingle<int>(query, new { Member_Old.UID }) == 0;
            }
        }


        public GameMacro GetMacros()
        {
            return _pangyaGameMacroRepository.GetByUID(Member_Old.UID);
        }
   
        public Character GetCurrentCharacter()
        {
            return Characters.FirstOrDefault(c => c.CID == UserEquip.CHARACTER_ID);
        }

        public Mascot GetCurrentMascot()
        {
            return Mascots.FirstOrDefault(m => m.MID == UserEquip.MASCOT_ID);
        }

        #endregion
    }
}
