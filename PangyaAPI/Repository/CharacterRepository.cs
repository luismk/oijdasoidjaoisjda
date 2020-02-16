using Dapper;
using PangyaAPI.Collections;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository
{
    public class CharacterRepository
    {
        private readonly string _connectionString;

        private CharacterEquipRepository _characterEquipRepository { get; set; }

        private CardEquipRepository _cardEquipRepository { get; set; }

        public CharacterRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
            _characterEquipRepository = new CharacterEquipRepository();
            _cardEquipRepository = new CardEquipRepository();
        }

        public IEnumerable<Character> GetByUid(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM [dbo].[Pangya_Character] WHERE UID = @UID";

                var characters = connection.Query<Character>(query, new { UID = uid });

                foreach (var character in characters)
                {
                    character.CharacterEquip = _characterEquipRepository.GetByCharacterId(character.CID);
                    character.CardEquipCollection = _cardEquipRepository.GetByCharacterId(character.CID);
                }

                return characters;
            }



        }
    }
}
