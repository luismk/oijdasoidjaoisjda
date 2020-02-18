using Pangya_Season7_GS.Handles_Packet;
using UGPangya.API.BinaryModels;

namespace UGPangya.LoginServer.Handles_Packet
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