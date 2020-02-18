using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_MATCH_DATA : PacketResult
    {
        public byte[] ShotData { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            ShotData = reader.ReadBytes(87);
        }
    }
}