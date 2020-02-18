using UGPangya.API;

namespace UGPangya.GameServer.Handles
{
    public class Handle_PLAYER_ENTER_TO_SHOP
    {
        public Handle_PLAYER_ENTER_TO_SHOP(Player player)
        {
            player.SendResponse(new byte[] {0x0E, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00});
        }
    }
}