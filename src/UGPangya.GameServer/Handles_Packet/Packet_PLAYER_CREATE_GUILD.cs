using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_CREATE_GUILD : PacketResult
    {
        public string GuildName { get; set; }

        public string GuildInfo { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            GuildName = reader.ReadPStr();
            GuildInfo = reader.ReadPStr();
        }
    }
}