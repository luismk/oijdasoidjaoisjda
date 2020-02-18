using UGPangya.API;
using UGPangya.API.BinaryModels;
using UGPangya.API.Repository;
using UGPangya.GameServer.Handles_Packet;

namespace UGPangya.GameServer.Handles
{
    public class Handle_PLAYER_GUILD_AVALIABLE : HandleBase<Packet_PLAYER_GUILD_AVALIABLE>
    {
        public Handle_PLAYER_GUILD_AVALIABLE(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var guildNameDisponivel = new GuildRepository().GuildNameAvaliable(PacketResult.GuildName);

            var response = new PangyaBinaryWriter();

            response.Write(new byte[] {0xB6, 0x01});

            if (guildNameDisponivel)
            {
                response.Write(1);
                response.WritePStr(PacketResult.GuildName);
            }
            else
            {
                response.Write(54510);
            }

            Player.SendResponse(response);
        }
    }
}