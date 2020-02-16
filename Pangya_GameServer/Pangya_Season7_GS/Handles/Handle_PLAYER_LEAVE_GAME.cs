using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_LEAVE_GAME : HandleBase<Packet_PLAYER_LEAVE_GAME>
    {
        public Handle_PLAYER_LEAVE_GAME(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            Player.Channel.Lobby.GameManager.Destroy(Player.Game);
            Player.SendResponse(new byte[] { 0x4C, 0x00, 0xFF, 0xFF });
            Player.Game = null;

            Player.Channel.Lobby.GameManager.List(Player);
        }
    }
}
