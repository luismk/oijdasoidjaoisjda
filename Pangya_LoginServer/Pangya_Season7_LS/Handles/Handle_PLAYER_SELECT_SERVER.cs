using Pangya_Season7_LS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.Handles;
using PangyaConnector.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_LS.Handles
{
    public class Handle_PLAYER_SELECT_SERVER : HandleBase<Packet_PLAYER_SELECT_SERVER>
    {
        public Handle_PLAYER_SELECT_SERVER(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            Console.WriteLine($" Player Selected Server ID: {PacketResult.ServerID}");

            Player.Response.Write(new byte[] { 0x03, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Player.Response.WritePStr(new MemberRepository().GetByUID(Player.Member.UID).AuthKey_Game);
            Player.SendResponse();

            //chave de recconexão?
            Player.Send(new byte[] { 0x00, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x0E, 0x00, 0x00, 0x00, 0x7B, 0x27, 0x00, 0x00 });
        }
    }
}
