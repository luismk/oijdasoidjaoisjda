using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_JOIN_GAME : PacketResult
    {
        public ushort RoomNumber { get; set; } 

        public string Password { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            RoomNumber = reader.ReadUInt16();
            Password = reader.ReadPStr();
        }
    }
}
