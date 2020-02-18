using Pangya_Season7_GS.Handles_Packet;
using UGPangya.API.BinaryModels;

namespace UGPangya.LoginServer.Handles_Packet
{
    public class Packet_PLAYER_SELECT_SERVER : PacketResult
    {
        public int ServerID { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            ServerID = reader.ReadInt32();
        }
    }
}