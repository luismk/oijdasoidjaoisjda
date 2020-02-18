using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_SHOT_DATA : PacketResult
    {
        public byte[] ResponseShot { get; set; }


        public int ConnectionId { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public ShotTypeEnum ShotType { get; set; }

        public short Unknown1 { get; set; } //2 bytes

        public uint Pang { get; set; }

        public uint BonusPang { get; set; }

        public int Unknown2 { get; set; } //4 bytes

        public byte[] MatchData { get; set; } //6 bytes

        public byte[] Unknown3 { get; set; } //11 bytes ou 17 bytes?  $10 = 16


        public override void Load(PangyaBinaryReader reader) { }

        public void Load(PangyaBinaryReader reader, byte[] roomKey)
        {
            //var result = reader.ReadBytes(56);

            var decrypted = DecryptShot(
                reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position))
                , roomKey);

            ResponseShot = decrypted.Take(38).ToArray();

            var readerDecripted = new PangyaBinaryReader(new MemoryStream(decrypted));

            ConnectionId = readerDecripted.ReadInt32();
            X = readerDecripted.ReadInt32();
            Y = readerDecripted.ReadInt32();
            Z = readerDecripted.ReadInt32();

            ShotType = (ShotTypeEnum)readerDecripted.ReadByte();
            Unknown1 = readerDecripted.ReadInt16();
            Pang = readerDecripted.ReadUInt32();
            BonusPang = readerDecripted.ReadUInt32();
            Unknown2 = readerDecripted.ReadInt32();
            MatchData = readerDecripted.ReadBytes(6);
            Unknown3 = readerDecripted.ReadBytes(17);
        }

        private byte[] DecryptShot(byte[] data, byte[] gameKey)
        {
            var decrypted = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                decrypted[i] = (byte)(data[i] ^ gameKey[i % 16]);
            }

            return decrypted;
        }

    }
}
