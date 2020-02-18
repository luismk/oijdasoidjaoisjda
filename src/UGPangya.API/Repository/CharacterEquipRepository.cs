using System.Data.SqlClient;
using Dapper;
using UGPangya.API.Repository.Models;

namespace UGPangya.API.Repository
{
    public class CharacterEquipRepository
    {
        private readonly string _connectionString;

        public CharacterEquipRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public CharacterEquip GetByCharacterId(int characterId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM [Pangya_Character_Equip] WHERE CHAR_IDX = @CHAR_IDX";

                return connection.QuerySingleOrDefault<CharacterEquip>(query, new {CHAR_IDX = characterId});
            }
        }
    }
}