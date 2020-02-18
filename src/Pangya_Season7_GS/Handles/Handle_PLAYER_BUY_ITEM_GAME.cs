using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_BUY_ITEM_GAME : HandleBase<Packet_PLAYER_BUY_ITEM_GAME>
    {
        public Handle_PLAYER_BUY_ITEM_GAME(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            ShowMessage(GameShopFlagEnum.ALREADY_HAVEITEM);
            //ShowMessage(GameShopFlagEnum.BUY_FAIL);
            //ShowMessage(GameShopFlagEnum.BUY_SUCCESS);
            //ShowMessage(GameShopFlagEnum.CANNOT_BUY_ITEM);
            //ShowMessage(GameShopFlagEnum.CANNOT_BUY_ITEM1);
            //ShowMessage(GameShopFlagEnum.COOKIE_NOTENOUGHT);
            //ShowMessage(GameShopFlagEnum.ITEM_CANNOT_PURCHASE);
            //ShowMessage(GameShopFlagEnum.ITEM_EXPIRED);
            //ShowMessage(GameShopFlagEnum.LEVEL_NOTENOUGHT);
            //ShowMessage(GameShopFlagEnum.OUT_OF_TIME);
            //ShowMessage(GameShopFlagEnum.PANG_NOTENOUGHT);
            //ShowMessage(GameShopFlagEnum.PASSWORD_WRONG);
            //ShowMessage(GameShopFlagEnum.TOO_MUCH_ITEM);
        }

        private void ShowMessage(GameShopFlagEnum Code)
        {
            var response = new PangyaBinaryWriter();

            response.Write(new byte[] { 0x68, 0x00 });
            response.Write((int)Code); //Show Succeed

            //if (Sucess)
            //{
            //    response.WriteUInt64(player.GetPang());
            //    response.WriteUInt64(player.GetCookie());
            //}

            Player.SendResponse(response.GetBytes());
        }
    }
}
