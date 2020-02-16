using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_LOADING_INFO : PacketResult
    {
        public byte Progress { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Progress = reader.ReadByte();
        }
    }
}
