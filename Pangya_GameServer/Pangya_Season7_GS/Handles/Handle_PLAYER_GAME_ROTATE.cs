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
    public class Handle_PLAYER_GAME_ROTATE : HandleBase<Packet_PLAYER_GAME_ROTATE>
    {
        public Handle_PLAYER_GAME_ROTATE(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var packet = new PangyaBinaryWriter();

            packet.Write(new byte[] { 0x56, 0x00 });
            packet.WriteUInt32(Player.ConnectionId);
            packet.Write(PacketResult.Angle);

            Player.Game.SendToAll(packet);
        }
    }
}
