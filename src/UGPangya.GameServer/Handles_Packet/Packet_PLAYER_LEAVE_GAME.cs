using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UGPangya.API;
using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_LEAVE_GAME : PacketResult
    {
        public override void Load(PangyaBinaryReader reader)
        {
        }
    }
}
