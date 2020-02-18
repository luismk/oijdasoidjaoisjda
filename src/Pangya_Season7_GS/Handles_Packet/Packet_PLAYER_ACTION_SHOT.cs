using PangyaAPI;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_ACTION_SHOT : PacketResult
    {
        public ShotTypeEnum ShotType { get; set; }

        public byte[] Un { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            ShotType = (ShotTypeEnum)reader.ReadUInt16();

            switch (ShotType)
            {
                case ShotTypeEnum.UNKOWN:
                    {
                        reader.Skip(9);
                        Un = reader.ReadBytes(62);
                    }
                    break;
                default:
                    Un = reader.ReadBytes(62);
                    break;
            }
        }
    }
}
