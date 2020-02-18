using UGPangya.API;
using UGPangya.API.Handles;
using UGPangya.LoginServer.Handles_Packet;

namespace UGPangya.LoginServer.Handles
{
    public class Handle_PLAYER_RECONNECT : HandleBase<Packet_PLAYER_RECONNECT>
    {
        public Handle_PLAYER_RECONNECT(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            //_player.Member_Old.AuthKey_Login = GenerateAuthKey();
            //_player.Member_Old.SaveChanges();

            //HandleLoginAuthKey();

            new Handle_Shared().ListarServidores(Player);
        }
    }
}