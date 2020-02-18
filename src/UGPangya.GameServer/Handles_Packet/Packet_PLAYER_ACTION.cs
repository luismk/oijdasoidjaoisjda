using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UGPangya.API;
using UGPangya.API.BinaryModels;
using UGPangya.API.Models;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_ACTION : PacketResult
    {
        public PlayerActionEnum Action { get; set; }


        public override void Load(PangyaBinaryReader reader)
        {
            Action = (PlayerActionEnum)reader.ReadByte();
        }
    }
}
