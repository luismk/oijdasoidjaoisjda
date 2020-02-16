using PangyaAPI;
using PangyaAPI.BinaryModels;
using System.Collections.Generic;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_BUY_ITEM_GAME : PacketResult
    {
        public GameShopEnum BuyType { get; set; }

        public ushort BuyTotal { get; set; }

        public List<ShopItem> BuyItens = new List<ShopItem>();

        public override void Load(PangyaBinaryReader reader)
        {
            BuyType = (GameShopEnum)reader.ReadByte();

            BuyTotal = reader.ReadUInt16();

            for (int Count = 0; Count <= BuyTotal - 1; Count++)
            {
                var item = new ShopItem();
                item.UN1 = reader.ReadInt32(); //PlayerCurrentMoney
                item.TypeId = reader.ReadInt32();
                item.TotalDay = reader.ReadInt16();
                item.UN2 = reader.ReadInt16();
                item.ItenQtd = reader.ReadInt32();
                item.PangPrice = reader.ReadInt32();
                item.CookiePrice = reader.ReadInt32();

                BuyItens.Add(item);
            }
        }

        public class ShopItem
        {
            public int UN1 { get; set; }

            public int TypeId { get; set; }

            public short TotalDay { get; set; }

            public short UN2 { get; set; }

            public int ItenQtd { get; set; }

            public int PangPrice { get; set; }

            public int CookiePrice { get; set; }
        }
    }

}
