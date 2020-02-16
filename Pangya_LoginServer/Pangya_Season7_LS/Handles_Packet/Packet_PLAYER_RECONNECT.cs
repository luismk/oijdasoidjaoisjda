using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_LS.Handles_Packet
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
