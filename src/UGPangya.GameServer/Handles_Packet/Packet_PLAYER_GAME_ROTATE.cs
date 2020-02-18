using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_GAME_ROTATE : PacketResult
    {
        public int Angle { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Angle = reader.ReadInt32();
        }
    }
}