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
    public class Handle_PLAYER_SET_NICKNAME : HandleBase<Packet_PLAYER_SET_NICKNAME>
    {
        public Handle_PLAYER_SET_NICKNAME(Player player) : base(player)
        {
            var updateSuccess = new MemberRepository().UpdateNickName(Player.Member.UID, PacketResult.Nickname);

            if (updateSuccess)
            {
                //Atualiza NickName
                Player.Member.Nickname = PacketResult.Nickname;

                //Redireciona usuário para tela de criação de character
                HandleCreateCharacter();
            }
        }

        public void HandleCreateCharacter()
        {
            Player.Response.Write(new byte[] { 0x0F, 0x00, 0x00 });
            Player.Response.WritePStr(Player.Member.UserName);
            Player.SendResponse();

            SendLoginMessage(LoginMessageEnum.CreateNickName);
        }

        private void SendLoginMessage(LoginMessageEnum msgType)
        {
            Player.Response.Write(new byte[] { 0x01, 0x00 });
            Player.Response.WriteUInt32((int)msgType);
            Player.Response.WriteByte(0);
            Player.SendResponse();
        }
    }
}
