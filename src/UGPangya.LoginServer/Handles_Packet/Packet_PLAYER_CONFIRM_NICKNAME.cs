using Pangya_Season7_GS.Handles_Packet;
using UGPangya.API.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UGPangya.LoginServer.Handles_Packet
{
    public class Packet_PLAYER_CONFIRM_NICKNAME : PacketResult
    {
        public string NickName { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            NickName = reader.ReadPStr();
        }
    }
}
