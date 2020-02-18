using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_GUILD_AVALIABLE : PacketResult
    {
        public string GuildName { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            GuildName = reader.ReadPStr(); 
        }
    }
}
