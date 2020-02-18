using UGPangya.API;
using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_ACTION : PacketResult
    {
        public PlayerActionEnum Action { get; set; }


        public override void Load(PangyaBinaryReader reader)
        {
            Action = (PlayerActionEnum) reader.ReadByte();
        }
    }
}