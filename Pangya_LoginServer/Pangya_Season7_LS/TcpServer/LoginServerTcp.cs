using PangyaAPI;
using PangyaAPI.Auth;
using PangyaAPI.Repository;
using System;
using System.Net.Sockets;

namespace Pangya_Season7_LS
{
    public class LoginServerTcp : TcpServer
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="port">TH = 10201, US = 10103</param>
        public LoginServerTcp(string ip, int port, int maxConnections) : base(ip, port, maxConnections)
        {
            Console.Title = $"Pangya Fresh UP ! LoginServer";
            IsOpen = true; //Define que o servidor está fechado
        }

        protected override AuthClient AuthServerConstructor()
        {
            return new AuthClient(name: "Unogames", type: AuthClientTypeEnum.LoginServer, port: 30303, key: "3493ef7ca4d69f54de682bee58be4f93");
        }

        protected override void SendKey(Player player)
        {
            player.DuplicatedLogin = false;

            //Envia packet com a chave do Player
            player.SendBytes(new byte[] { 0x00, 0x0B, 0x00, 0x00, 0x00, 0x00, player.Key, 0x00, 0x00, 0x00, 0x0F, 0x27, 0x00, 0x00 });
        }

        public void ShowConsoleHelp()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Bem vindo(a) ao Login-Server!" + Environment.NewLine);

            Console.WriteLine("Veja os comandos disponíveis do console:" + Environment.NewLine);

            Console.WriteLine("help      | exibe os comandos do console");
            Console.WriteLine("start     | abre o servidor para os jogadores");
            Console.WriteLine("stop      | fecha o servidor para novos jogadores");
            Console.WriteLine("broadcast | exibe mensagem para usuários que estão efetuando login");

            Console.WriteLine("clear     | limpa console");
            Console.WriteLine("cls       | limpa console");
            Console.WriteLine("quit      | fecha o servidor");

            Console.WriteLine(Environment.NewLine);
        }

        protected override void OnAuthServerPacketReceive(AuthClient client, AuthPacket packet)
        {
            switch (packet.Id)
            {
                case AuthPacketEnum.SERVER_KEEPALIVE: //KeepAlive
                    {
                        //Console.WriteLine("KeepAlive");
                    }
                    break;
                default:
                    //Console.WriteLine("AuthPacketDefault: " + packet.Id);
                    break;
            }
        }
    }
}
