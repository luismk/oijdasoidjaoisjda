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
    public class Handle_PLAYER_CONFIRM_NICKNAME : HandleBase<Packet_PLAYER_CONFIRM_NICKNAME>
    {
        public Handle_PLAYER_CONFIRM_NICKNAME(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var nomesNaoPermitidos = new string[]
            {
                "adm",
                "gm",
            };

            //Verifica se o nickname possui palavras não permitidas
            if (nomesNaoPermitidos.Any(valorNaoPermitido => PacketResult.NickName.ToLower().Contains(valorNaoPermitido.ToLower())))
            {
                MessageBoxNickName(ConfirmNickNameMessageEnum.PalavasInapropriadas, PacketResult.NickName);
            }

            var nickNameInUse = new ProcedureRepository().USP_NICKNAME_CHECK(PacketResult.NickName);

            //Ocorreu Um Erro
            if (nickNameInUse == 0)
            {
                MessageBoxNickName(ConfirmNickNameMessageEnum.OcorreuUmErro, PacketResult.NickName);
            }
            //indisponivel
            if (nickNameInUse == 2)
            {
                MessageBoxNickName(ConfirmNickNameMessageEnum.Indisponivel, PacketResult.NickName);
            }
            //disponivel
            if (nickNameInUse == 1)
            {
                MessageBoxNickName(ConfirmNickNameMessageEnum.Disponivel, PacketResult.NickName);
            }
        }

        private void MessageBoxNickName(ConfirmNickNameMessageEnum msgType, string nickname)
        {
            Console.WriteLine(DateTime.Now.ToString() + $" CONFIRM_NICK: {msgType}");

            Player.Response.Write(new byte[] { 0x0E, 0x00 });
            Player.Response.WriteUInt32((int)msgType);
            if (msgType == ConfirmNickNameMessageEnum.Disponivel)
            {
                Player.Response.WritePStr(nickname);
            }
            Player.SendResponse();
        }
    }
}
