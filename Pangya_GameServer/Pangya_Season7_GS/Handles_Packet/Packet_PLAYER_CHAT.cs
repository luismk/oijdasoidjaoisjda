using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_CHAT : PacketResult
    {
        public string Nickname { get; set; }

        public string Message { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Nickname = reader.ReadPStr();
            Message = reader.ReadPStr();
        }
    }
}
