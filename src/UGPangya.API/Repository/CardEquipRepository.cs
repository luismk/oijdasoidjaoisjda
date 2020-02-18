using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using UGPangya.API.Collections;
using UGPangya.API.Repository.Models;

namespace UGPangya.API.Repository
{
    public class CardEquipRepository
    {
        private readonly string _connectionString;

        public CardEquipRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public IEnumerable<CardEquip> GetByUserId(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM [Pangya_Card_Equip] WHERE UID = @UID";

                return connection.Query<CardEquip>(query, new {UID = uid});
            }
        }

        public CardEquipCollection GetByCharacterId(int characterId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM [Pangya_Card_Equip] WHERE CID = @CID";

                return new CardEquipCollection(connection.Query<CardEquip>(query, new {CID = characterId}));
            }
        }
    }
}