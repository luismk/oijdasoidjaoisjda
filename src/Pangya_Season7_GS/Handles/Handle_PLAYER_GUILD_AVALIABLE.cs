using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_GUILD_AVALIABLE : HandleBase<Packet_PLAYER_GUILD_AVALIABLE>
    {
        public Handle_PLAYER_GUILD_AVALIABLE(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            bool guildNameDisponivel = new GuildRepository().GuildNameAvaliable(PacketResult.GuildName);

            var response = new PangyaBinaryWriter();

            response.Write(new byte[] { 0xB6, 0x01 });

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
