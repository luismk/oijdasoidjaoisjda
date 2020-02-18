using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Models;

namespace Pangya_Season7_GS.Handles_Packet
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
