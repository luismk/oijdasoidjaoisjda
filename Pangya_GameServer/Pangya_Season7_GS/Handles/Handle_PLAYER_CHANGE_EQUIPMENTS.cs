using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    /// <summary>
    /// INCOMPLETO
    /// </summary>
    public class Handle_PLAYER_CHANGE_EQUIPMENTS : HandleBase<Packet_PLAYER_CHANGE_EQUIPMENTS>
    {
        private UserEquipRepository _repository = new UserEquipRepository();


        public Handle_PLAYER_CHANGE_EQUIPMENTS(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            Console.WriteLine("Action: " + PacketResult.Action);

            switch (PacketResult.Action)
            {
                case Packet_PLAYER_CHANGE_EQUIPMENTS.ChangeEquipmentEnum.SetEquip_Char:
                    break;
                case Packet_PLAYER_CHANGE_EQUIPMENTS.ChangeEquipmentEnum.SetIndexCaddie:
                    {
                        Console.WriteLine("Change to CaddieId: " + PacketResult.CaddieId ?? "NULL FOR CADDIEID");
                        Player.UserEquip.CADDIE = PacketResult.CaddieId;
                    }
                    break;
                case Packet_PLAYER_CHANGE_EQUIPMENTS.ChangeEquipmentEnum.SetItensPlay:
                    break;
                case Packet_PLAYER_CHANGE_EQUIPMENTS.ChangeEquipmentEnum.SetGolfEQP:
                    break;
                case Packet_PLAYER_CHANGE_EQUIPMENTS.ChangeEquipmentEnum.SetDecoration:
                    break;
                case Packet_PLAYER_CHANGE_EQUIPMENTS.ChangeEquipmentEnum.SetIndexChar:
                    break;
                case Packet_PLAYER_CHANGE_EQUIPMENTS.ChangeEquipmentEnum.SetIndexMascot:
                    {
                        Console.WriteLine("Change to MascotId: " + PacketResult.MascoteId ?? "NULL FOR MascotId");

                        Player.UserEquip.MASCOT_ID = PacketResult.MascoteId;
                    }
                    break;
                case Packet_PLAYER_CHANGE_EQUIPMENTS.ChangeEquipmentEnum.SetCharCutin:
                    break;
            }

            _repository.Update(Player.UserEquip);

        }
    }
}
