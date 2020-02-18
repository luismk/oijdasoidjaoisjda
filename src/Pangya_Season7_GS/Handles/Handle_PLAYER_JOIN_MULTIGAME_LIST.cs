using PangyaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_JOIN_MULTIGAME_LIST
    {
        private Player _player { get; set; }

        public Handle_PLAYER_JOIN_MULTIGAME_LIST(Player player)
        {
            _player = player;
            Handle();
        }

        private void Handle()
        {
            _player.Channel.Lobby.List(_player);
            _player.Channel.Lobby.GameManager.List(_player);
            _player.SendResponse(new byte[] { 0xF5, 0x00 });
        }
    }
}
