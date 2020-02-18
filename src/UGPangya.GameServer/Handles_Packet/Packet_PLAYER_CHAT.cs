using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_CHAT : PacketResult
    {
        public string Nickname { get; set; }

        public string Message { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Nickname = reader.ReadPStr();
            Message = reader.ReadPStr();
        }
    }
}