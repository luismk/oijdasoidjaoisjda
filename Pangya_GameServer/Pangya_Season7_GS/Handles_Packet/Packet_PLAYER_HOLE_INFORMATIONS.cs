using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_HOLE_INFORMATIONS : PacketResult
    {
        public int HolePosition { get; private set; }

        private int Unknown { get; set; }

        public byte Par { get; private set; }

        public float A { get; private set; }

        public float B { get; private set; }

        public float X { get; private set; }

        public float Z { get; private set; }

        public override void Load(PangyaBinaryReader reader)
        {
            HolePosition = reader.ReadInt32();
            Unknown = reader.ReadInt32();
            Par = reader.ReadByte();
            A = reader.ReadSingle();
            B = reader.ReadSingle();
            X = reader.ReadSingle();
            Z = reader.ReadSingle();
        }
    }
}
