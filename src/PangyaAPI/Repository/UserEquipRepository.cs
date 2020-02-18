using Dapper;
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
    public class UserEquipRepository
    {
        private readonly string _connectionString;

        public UserEquipRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public  UserEquip GetByUID(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<UserEquip>("SELECT * FROM Pangya_User_Equip WHERE UID = @UID", new { UID = uid });
            }
        }

        public bool Update(UserEquip model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE [dbo].[Pangya_User_Equip] SET 
                [CHARACTER_ID] = @CHARACTER_ID, 
                [CLUB_ID] = @CLUB_ID, 
                [BALL_ID] = @BALL_ID, 
                [MASCOT_ID] = @MASCOT_ID, 
                [CADDIE] = @CADDIE, 
                [TITLE_TYPEID] = @TITLE_TYPEID,
                [ITEM_SLOT_1] = @ITEM_SLOT_1, 
                [ITEM_SLOT_2] = @ITEM_SLOT_2, 
                [ITEM_SLOT_3] = @ITEM_SLOT_3, 
                [ITEM_SLOT_4] = @ITEM_SLOT_4, 
                [ITEM_SLOT_5] = @ITEM_SLOT_5, 
                [ITEM_SLOT_6] = @ITEM_SLOT_6, 
                [ITEM_SLOT_7] = @ITEM_SLOT_7, 
                [ITEM_SLOT_8] = @ITEM_SLOT_8, 
                [ITEM_SLOT_9] = @ITEM_SLOT_9, 
                [ITEM_SLOT_10] = @ITEM_SLOT_10,
                [POSTER_1] = @POSTER_1, 
                [POSTER_2] = @POSTER_2 

                WHERE UID = @UID";

                var result = connection.Execute(query, model);

                return result == 1;
            }
        }
    }
}
