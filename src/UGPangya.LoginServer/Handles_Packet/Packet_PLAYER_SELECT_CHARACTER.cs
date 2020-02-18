using Pangya_Season7_GS.Handles_Packet;
using UGPangya.API.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UGPangya.LoginServer.Handles_Packet
{
    public class Packet_PLAYER_SELECT_CHARACTER : PacketResult
    {
        public int CHAR_TYPEID { get; set; }
        public ushort HAIR_COLOR { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            CHAR_TYPEID = reader.ReadInt32();
            HAIR_COLOR = reader.ReadUInt16();
        }
    }
}
