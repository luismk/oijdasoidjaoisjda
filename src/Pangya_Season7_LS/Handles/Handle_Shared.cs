using PangyaAPI;
using PangyaConnector.Repository;
using System;
using System.Linq;

namespace Pangya_Season7_LS.Handles
{
    public class Handle_Shared
    {
        public void SendPlayerLoggedOnData(Player player)
        {
            HandleLoginAuthKey(player);

            HandleGame_Macros(player);

            HandlePlayerInformation(player);

            ListarServidores(player);

            HandleListMessengerServer(player);
        }

        public void ListarServidores(Player player)
        {
            var gameServers = new PangyaServerRepository().GetGameServers();

            player.Response.Write(new byte[] { 0x02, 0x00 });
            player.Response.WriteByte(gameServers.Count());
            if (gameServers.Count() > 0)
            {
                foreach (var servidor in gameServers)
                {
                    player.Response.WriteStr(servidor.Name, 40);// aqui no caso deve ser 16               
                    player.Response.Write(servidor.ServerID);//server UID
                    player.Response.WriteUInt32(5000); //suporte maximo de jogadores no servidor simultaneamente
                    player.Response.Write(player.Server.Players.Count); //Total de jogadores no servidor atualmente ou simultaneamente(limitador)
                    player.Response.WriteStr(servidor.IP, 18);
                    player.Response.Write((int)servidor.Port);
                    player.Response.WriteUInt32(2048); //imagem do grand prix 2048, manto 16               
                    player.Response.WriteUInt32(1); //Angelic Number Count
                    player.Response.Write((ushort)servidor.ImgEvent);
                    player.Response.Write(new byte[] { 0x00, 0x00, 0x64, 0x00, 0x00, 0x00 }); //tem alguma coisa aqui
                    player.Response.Write((ushort)servidor.ImgNo);
                }
            }

            player.SendResponse();
        }

        private void HandleLoginAuthKey(Player player)
        {
            player.Response.Write(new byte[] { 0x10, 0x00 });
            player.Response.WritePStr(player.Member.AuthKey_Login);
            player.SendResponse();
        }

        private void HandleGame_Macros(Player player)
        {
            var macros = new GameMacroRepository().GetByUID(player.Member.UID);

            player.Response.Write(new byte[] { 0x06, 0x00 });
            player.Response.WriteStr(macros.Macro1, 64);
            player.Response.WriteStr(macros.Macro2, 64);
            player.Response.WriteStr(macros.Macro3, 64);
            player.Response.WriteStr(macros.Macro4, 64);
            player.Response.WriteStr(macros.Macro5, 64);
            player.Response.WriteStr(macros.Macro6, 64);
            player.Response.WriteStr(macros.Macro7, 64);
            player.Response.WriteStr(macros.Macro8, 64);
            player.Response.WriteStr(macros.Macro9, 64);
            player.SendResponse();
        }

        private void HandlePlayerInformation(Player player)
        {
            var statistics = new UserStatisticsRepository().GetByUID(player.Member.UID);

            player.Response.Write(new byte[] { 0x01, 0x00, 0x00 });
            player.Response.WritePStr(player.Member.UserName);
            player.Response.Write(player.Member.UID);
            player.Response.WriteUInt32(player.Member.Sex);
            player.Response.WriteUInt32(statistics.Game_Level);//Level, uint32
            player.Response.WriteUInt32(10);
            player.Response.Write((ushort)12);
            player.Response.WritePStr(player.Member.Nickname);
            player.SendResponse();
        }

        private void HandleListMessengerServer(Player player)
        {
            var serverRepository = new PangyaServerRepository();

            var messengerServers = serverRepository.GetMessengerServers();

            //Messenger
            player.Response.Write(new byte[] { 0x09, 0x00 });
            player.Response.Write((byte)messengerServers.Count());
            if (messengerServers.Count() > 0)
            {
                foreach (var servidor in messengerServers)
                {
                    player.Response.WriteStr(servidor.Name, 40);
                    player.Response.Write(servidor.ServerID);
                    player.Response.Write((UInt32)5000); //Max Users
                    player.Response.Write(player.Server.Players.ToList().Count);
                    player.Response.WriteStr(servidor.IP, 18);
                    player.Response.Write((UInt32)servidor.Port);
                    player.Response.Write((UInt32)4096);
                }
            }
            player.SendResponse();
        }

        /// <summary>
        /// GERA UMA CHAVE DE AUTENTIFICAÇÃO EM STRING NO TAMANHO 7 caracters
        /// </summary>
        /// <returns></returns>
        public string GenerateAuthKey()
        {
            return Guid.NewGuid().ToString()
                .ToUpper()
                .Replace("-", string.Empty).Substring(0, 7);
        }
    }
}
