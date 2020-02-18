using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_LS.Handles_Packet
{
    public class Packet_PLAYER_LOGIN : PacketResult
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            //Credenciais do usuário
            UserName = reader.ReadPStr();
            Password = reader.ReadPStr();
        }
    }
}
