using Pangya_Season7_GS.Handles_Packet;
using UGPangya.API.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UGPangya.LoginServer.Handles_Packet
{
    public class Packet_PLAYER_RECONNECT : PacketResult
    {
        public string Username { get; set; }

        public int UID { get; set; }

        public string AuthKey_Game { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Username = reader.ReadPStr();
            UID = reader.ReadInt32();
            AuthKey_Game = reader.ReadPStr();
        }
    }
}
