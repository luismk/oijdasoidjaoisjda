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
    public class Handle_PLAYER_LOGIN : HandleBase<Packet_PLAYER_LOGIN>
    {
        //Repositories
        private MemberRepository _repo = new MemberRepository();

        public Handle_PLAYER_LOGIN(Player player) : base(player)
        {
            //Se o servidor estiver fechado
            if (Server.IsOpen == false)
            {
                LoginResultMessage(LoginMessageEnum.ServerInMaintenance);

                Player.Disconnect();
                return;
            }

            if (_repo.UserIsValid(PacketResult.UserName, PacketResult.Password) == false)
            {
                LoginResultMessage(LoginMessageEnum.UsuarioIncorreto);

                Player.Disconnect();
                return;
            }

            if (_repo.UserIsBan(PacketResult.UserName))
            {
                LoginResultMessage(LoginMessageEnum.Banido);
                return;
            }

            Player.LoadMember(PacketResult.UserName);

            Player.Member.AuthKey_Login = new Handle_Shared().GenerateAuthKey();
            Player.Member.AuthKey_Game = new Handle_Shared().GenerateAuthKey();

            _repo.UpdateAuthKey(Player.Member.UID, Player.Member.AuthKey_Login, Player.Member.AuthKey_Game);


            if (string.IsNullOrEmpty(Player.Member.Nickname))
            {
                LoginResultMessage(LoginMessageEnum.CreateNickName_US);
                return;
            }

            ////Usuario em Uso
            //if (PlayerData.AccountInUse())
            //{
            //    this.DuplicatedLogin = true;
            //    SendLoginMessage(LoginMessageEnum.UsuarioEmUso);
            //    return;
            //}

            //salva dados
            //Player.Member.IPAddress = Player.GetIpAdress();


            //Primeiro Login = 0
            if (_repo.IsFirstSet(Player.Member.UID))
            {
                RedirectToCreateCharacter();
                return;
            }
            else
            {
                new Handle_Shared().SendPlayerLoggedOnData(Player);
            }
        }

        /// <summary>
        /// Envia mensagem de resultado ao efetuar login
        /// </summary>
        /// <param name="msgType">Tipo da mensagem</param>
        private void LoginResultMessage(LoginMessageEnum msgType)
        {
            Player.Response.Write(new byte[] { 0x01, 0x00 });
            Player.Response.WriteUInt32((int)msgType);
            Player.Response.WriteByte(0);
            Player.SendResponse();
        }

        /// <summary>
        /// Redireciona usuário para Tela de Criar um Personagem
        /// </summary>
        private void RedirectToCreateCharacter()
        {
            Player.Response.Write(new byte[] { 0x0F, 0x00, 0x00 });
            Player.Response.WritePStr(PacketResult.UserName);
            Player.SendResponse();

            LoginResultMessage(LoginMessageEnum.CreateNickName);
        }


     
    }
}
