using System;
using System.Linq;
using UGPangya.API;
using UGPangya.API.Handles;
using UGPangya.Connector.Repository;
using UGPangya.LoginServer.Handles_Packet;

namespace UGPangya.LoginServer.Handles
{
    public class Handle_PLAYER_CONFIRM_NICKNAME : HandleBase<Packet_PLAYER_CONFIRM_NICKNAME>
    {
        public Handle_PLAYER_CONFIRM_NICKNAME(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var nomesNaoPermitidos = new[]
            {
                "adm",
                "gm"
            };

            //Verifica se o nickname possui palavras não permitidas
            if (nomesNaoPermitidos.Any(valorNaoPermitido =>
                PacketResult.NickName.ToLower().Contains(valorNaoPermitido.ToLower())))
                MessageBoxNickName(ConfirmNickNameMessageEnum.PalavasInapropriadas, PacketResult.NickName);

            var nickNameInUse = new ProcedureRepository().USP_NICKNAME_CHECK(PacketResult.NickName);

            //Ocorreu Um Erro
            if (nickNameInUse == 0) MessageBoxNickName(ConfirmNickNameMessageEnum.OcorreuUmErro, PacketResult.NickName);
            //indisponivel
            if (nickNameInUse == 2) MessageBoxNickName(ConfirmNickNameMessageEnum.Indisponivel, PacketResult.NickName);
            //disponivel
            if (nickNameInUse == 1) MessageBoxNickName(ConfirmNickNameMessageEnum.Disponivel, PacketResult.NickName);
        }

        private void MessageBoxNickName(ConfirmNickNameMessageEnum msgType, string nickname)
        {
            Console.WriteLine(DateTime.Now + $" CONFIRM_NICK: {msgType}");

            Player.Response.Write(new byte[] {0x0E, 0x00});
            Player.Response.WriteUInt32((int) msgType);
            if (msgType == ConfirmNickNameMessageEnum.Disponivel) Player.Response.WritePStr(nickname);
            Player.SendResponse();
        }
    }
}