using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_SELECT_LOBBY : HandleBase<Packet_PLAYER_SELECT_LOBBY>
    {
        public Handle_PLAYER_SELECT_LOBBY(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var channel = Server.Channels.FirstOrDefault(c => c.Id == PacketResult.ChannelId);

            Packet_PLAYER_SELECT_LOBBY.ChannelResult result;

            if (channel == null)
            {
                result = Packet_PLAYER_SELECT_LOBBY.ChannelResult.CannotFind;
            }
            else if (channel.IsFull())
            {
                result = Packet_PLAYER_SELECT_LOBBY.ChannelResult.IsFull;
            }
            else
            {
                //Se o player já está em um canal
                if(Player.Channel != null)
                {
                    //Remove Player do canal
                    Player.Channel.Players.Model.Remove(Player);
                }

                channel.PlayerJoin(Player);

                result = Packet_PLAYER_SELECT_LOBBY.ChannelResult.AllowAcess;
            }

            Player.SendResponse(PacketResult.GetChannelResult(result));
        }
    }
}
