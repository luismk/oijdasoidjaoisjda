using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_Handle_PLAYER_PAUSE_GAME : PacketResult
    {
        public enum StatusEnum
        {
            Stop = 0x01,
            Play = 0x00,
        }

        public StatusEnum Status { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Status =  (StatusEnum)reader.ReadByte();
        }
    }
}
