using Pangya_Season7_LS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.Handles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_LS.Handles
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
