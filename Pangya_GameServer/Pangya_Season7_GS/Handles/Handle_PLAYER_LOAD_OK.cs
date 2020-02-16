
using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_LOAD_OK : HandleBase<Packet_PLAYER_LOAD_OK>
    {
        public Handle_PLAYER_LOAD_OK(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            Player.SendResponse(new byte[] { 0x8D, 0x00, 0x40, 0x1F, 0x00, 0x00 });
            Player.SendResponse(new byte[] { 0x53, 0x00, 0x00, 0x00, 0x00, 0x00 });//conexao id apos id de resposta do packet (ushort)
        }
    }
}
