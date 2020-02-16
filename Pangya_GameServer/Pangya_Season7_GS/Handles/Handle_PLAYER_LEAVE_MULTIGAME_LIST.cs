using PangyaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_LEAVE_MULTIGAME_LIST
    {
        private Player _player { get; set; }

        public Handle_PLAYER_LEAVE_MULTIGAME_LIST(Player player)
        {
            _player = player;
            Handle();
        }

        private void Handle()
        {
            _player.Channel.Lobby.Destroy(_player);

            _player.SendResponse(new byte[] { 0xF6, 0x00 });
        }
    }
}
