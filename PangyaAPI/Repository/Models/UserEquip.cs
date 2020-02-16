using PangyaAPI.BinaryModels;

namespace PangyaAPI.Repository.Models
{
    public class UserEquip
    {
        private UserEquipRepository _userEquipRepository = new UserEquipRepository();

        public int UID { get; set; }

        public int CHARACTER_ID { get; set; }

        public int CLUB_ID { get; set; }

        public int BALL_ID { get; set; }

        public int MASCOT_ID { get; set; }

        public int CADDIE { get; set; }

        public int ITEM_SLOT_1 { get; set; }

        public int ITEM_SLOT_2 { get; set; }

        public int ITEM_SLOT_3 { get; set; }

        public int ITEM_SLOT_4 { get; set; }

        public int ITEM_SLOT_5 { get; set; }

        public int ITEM_SLOT_6 { get; set; }

        public int ITEM_SLOT_7 { get; set; }

        public int ITEM_SLOT_8 { get; set; }

        public int ITEM_SLOT_9 { get; set; }

        public int ITEM_SLOT_10 { get; set; }

        public int SKIN_BACKGROUND_ID { get; set; }

        public int SKIN_FRAME_ID { get; set; }

        public int SKIN_STICKER_ID { get; set; }

        public int SKIN_SLOT_ID { get; set; }

        public int CUTIN_ID { get; set; }

        public int TITLE_ID { get; set; }

        public int SKIN_BACKGROUND_TYPEID { get; set; }

        public int SKIN_FRAME_TYPEID { get; set; }

        public int SKIN_STICKER_TYPEID { get; set; }

        public int SKIN_SLOT_TYPEID { get; set; }

        public int CUTIN_TYPEID { get; set; }

        public int TITLE_TYPEID { get; set; }

        public int POSTER_1 { get; set; }

        public int POSTER_2 { get; set; }




        public bool SaveChanges()
        {
            return _userEquipRepository.Update(this);
        }

        public byte[] GetItemSlotData()
        {
            var result = new PangyaBinaryWriter();
            result.Write(ITEM_SLOT_1);
            result.Write(ITEM_SLOT_2);
            result.Write(ITEM_SLOT_3);
            result.Write(ITEM_SLOT_4);
            result.Write(ITEM_SLOT_5);
            result.Write(ITEM_SLOT_6);
            result.Write(ITEM_SLOT_7);
            result.Write(ITEM_SLOT_8);
            result.Write(ITEM_SLOT_9);
            result.Write(ITEM_SLOT_10);
            return result.GetBytes();
        }

        public byte[] SendToolbar(Player player)
        {
            var response = new PangyaBinaryWriter();
            //TOOLBAR
            response.Write(new byte[] { 0x72, 0x00 });
            response.Write((uint)CADDIE);
            response.Write((uint)CHARACTER_ID);
            response.Write((uint)CLUB_ID);
            response.Write((uint)BALL_ID);
            response.Write((uint)ITEM_SLOT_1);
            response.Write((uint)ITEM_SLOT_2);
            response.Write((uint)ITEM_SLOT_3);
            response.Write((uint)ITEM_SLOT_4);
            response.Write((uint)ITEM_SLOT_5);
            response.Write((uint)ITEM_SLOT_6);
            response.Write((uint)ITEM_SLOT_7);
            response.Write((uint)ITEM_SLOT_8);
            response.Write((uint)ITEM_SLOT_9);
            response.Write((uint)ITEM_SLOT_10);
            response.Write((uint)SKIN_BACKGROUND_ID); //BACKGROUND_ID
            response.Write((uint)SKIN_FRAME_ID); //FRAME_ID
            response.Write((uint)SKIN_STICKER_ID); //STICKER_ID
            response.Write((uint)SKIN_SLOT_ID); //SLOT_ID
            response.Write(0); //desconhecido id
            response.Write((uint)TITLE_ID); //Title IDX
            response.Write((uint)SKIN_BACKGROUND_TYPEID); //SKIN_BACKGROUND_TYPEID
            response.Write((uint)SKIN_FRAME_TYPEID); //SKIN_FRAME_TYPEID
            response.Write((uint)SKIN_STICKER_TYPEID); //SKIN_STICKER_TYPEID
            response.Write((uint)SKIN_SLOT_TYPEID); //SKIN_SLOT_TYPEID
            response.Write(0); //desconhecido type id
            response.Write((uint)TITLE_TYPEID); //Title TypeID
            response.Write((uint)MASCOT_ID);
            response.Write((uint)POSTER_1);
            response.Write((uint)POSTER_2);

            player.SendResponse(response.GetBytes());

            return response.GetBytes();
        }

        /// <summary>
        /// [24 bytes]
        /// </summary>
        public byte[] GetDecorationData()
        {
            var result = new PangyaBinaryWriter();
            result.Write(SKIN_BACKGROUND_TYPEID);
            result.Write(SKIN_FRAME_TYPEID);
            result.Write(SKIN_STICKER_TYPEID);
            result.Write(SKIN_SLOT_TYPEID);
            result.Write(CUTIN_TYPEID);
            result.Write(TITLE_TYPEID);
            return result.GetBytes();
        }
    }
}
