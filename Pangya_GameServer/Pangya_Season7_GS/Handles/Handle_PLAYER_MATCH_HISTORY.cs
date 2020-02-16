using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
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
