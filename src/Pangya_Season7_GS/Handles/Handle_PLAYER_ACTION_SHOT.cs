using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_ACTION_SHOT : HandleBase<Packet_PLAYER_ACTION_SHOT>
    {
        public Handle_PLAYER_ACTION_SHOT(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0x55, 0x00 });
            result.Write(Player.ConnectionId);
            result.Write(PacketResult.Un);

            Player.Game.SendToAll(result);
        }
    }
}
