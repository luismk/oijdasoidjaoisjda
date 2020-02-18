using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_PRESS_READY : PacketResult
    {
        public bool Ready { get; set; }
        public override void Load(PangyaBinaryReader reader)
        {
            Ready = reader.ReadBoolean();
        }
    }
}
