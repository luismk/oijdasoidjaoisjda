using Pangya_Season7_GS.Handles_Packet;
using UGPangya.API.BinaryModels;

namespace UGPangya.LoginServer.Handles_Packet
{
    public class Packet_PLAYER_SET_NICKNAME : PacketResult
    {
        public string Nickname { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Nickname = reader.ReadPStr();
        }
    }
}