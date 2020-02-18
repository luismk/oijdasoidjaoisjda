using UGPangya.API;

namespace UGPangya.GameServer.Handles
{
    public class Handle_PLAYER_LEAVE_MULTIGAME_LIST
    {
        public Handle_PLAYER_LEAVE_MULTIGAME_LIST(Player player)
        {
            _player = player;
            Handle();
        }

        private Player _player { get; }

        private void Handle()
        {
            _player.Channel.Lobby.Destroy(_player);

            _player.SendResponse(new byte[] {0xF6, 0x00});
        }
    }
}