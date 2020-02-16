using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_PAUSE_GAME : HandleBase<Packet_Handle_PLAYER_PAUSE_GAME>
    {
        public Handle_PLAYER_PAUSE_GAME(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var response = new PangyaBinaryWriter();

            response.Write(new byte[] { 0x8B, 0x00 });
            response.Write(Player.ConnectionId);
            response.Write((byte)PacketResult.Status);

            Player.Game.SendToAll(response);

        }
    }
}
