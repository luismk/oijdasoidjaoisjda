using UGPangya.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UGPangya.GameServer.Handles_Packet;

namespace UGPangya.GameServer.Handles
{
    public class Handle_PLAYER_MATCH_HISTORY : HandleBase<Packet_PLAYER_MATCH_HISTORY>
    {
        public Handle_PLAYER_MATCH_HISTORY(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            foreach (var file in Directory.GetFiles(@"C:\Users\Administrador\Desktop\Packets\PLAYER_MATCH_HISTORY"))
            {
                Player.SendResponse(File.ReadAllBytes(file));
            }

        }
    }
}
