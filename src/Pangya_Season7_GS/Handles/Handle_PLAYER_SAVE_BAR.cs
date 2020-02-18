using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_SAVE_BAR : HandleBase<Packet_PLAYER_SAVE_BAR>
    {
        private UserEquipRepository _repository = new UserEquipRepository();

        public Handle_PLAYER_SAVE_BAR(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            //Atualiza Character Atual
            Player.UserEquip.CHARACTER_ID = PacketResult.Id;

            //Atualiza Banco de dados
            _repository.Update(Player.UserEquip);

            //OBS.: Aparentemente o response é desnecessário para este Handle
            var response = new PangyaBinaryWriter();

            response.Write(new byte[] { 0x4B, 0x00 });
            response.Write(0);
            response.Write((byte)PacketResult.Action);
            response.Write(Player.ConnectionId);

            //Envia dados do character atual
            response.Write(Player.Characters.GetCharacterData(PacketResult.Id));

            Player.SendResponse(response.GetBytes());
        }

         //switch (PacketResult.Action)
         //   {
         //       case Packet_PLAYER_SAVE_BAR.ChangeEquipmentEnumB.GetCaddieSelected:
         //           {
         //               //SetCaddie;
         //               response.Write(Player.Caddies.GetCaddieData());
         //           }
         //           break;
         //       case Packet_PLAYER_SAVE_BAR.ChangeEquipmentEnumB.GetBallSelected:
         //           {
         //               //SetBallTypeID
         //               response.Write(PacketResult.Id);
         //           }
         //           break;
         //       case Packet_PLAYER_SAVE_BAR.ChangeEquipmentEnumB.GetClubSelected:
         //           {
         //               //SetClubIndex
         //               //response.Write(Player.UserEquip.GetClubData());//clubdata temp
         //           }
         //           break;
         //       case Packet_PLAYER_SAVE_BAR.ChangeEquipmentEnumB.GetCharSelected:
         //           {

         //           }
         //           break;
         //       case Packet_PLAYER_SAVE_BAR.ChangeEquipmentEnumB.GetMascotSelected:
         //           {
         //               //SetMascotIndex
         //               response.Write(Player.Mascots.GetMascotData());
         //           }
         //           break;
         //       case Packet_PLAYER_SAVE_BAR.ChangeEquipmentEnumB.GameStart:
         //           {
         //               //player.HandleAcquireData();
         //           }
         //           break;
         //   }
    }
}
