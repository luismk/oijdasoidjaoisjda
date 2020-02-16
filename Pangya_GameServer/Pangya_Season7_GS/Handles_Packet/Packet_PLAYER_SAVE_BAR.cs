using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_SAVE_BAR : PacketResult
    {

        public ChangeEquipmentEnumB Action { get; set; }

        public int Id { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Action = (ChangeEquipmentEnumB)reader.ReadByte();
            Id = reader.ReadInt32();
        }
    }
}
