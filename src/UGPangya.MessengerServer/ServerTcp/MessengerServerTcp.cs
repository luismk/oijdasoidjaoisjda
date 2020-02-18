using System;
using System.Net.Sockets;
using UGPangya.API;
using UGPangya.API.Auth;

namespace UGPangya.MessengerServer
{
    public class MessengerServerTcp : TcpServer
    {
        public MessengerServerTcp(string ip, int port, int maxConnections) : base(ip, port, maxConnections)
        {
            Console.Title = "Pangya Fresh UP! Messenger Server";
        }

        protected override Player CreatePlayer(TcpClient tcp)
        {
            var player = new MPlayer(tcp);

            Players.Add(player);

            return player;
        }

        protected override void SendKey(Player player)
        {
            //Gera Packet com chave de criptografia (posisão 7)
            var packetKey = new byte[] {0x00, 0x09, 0x00, 0x00, 0x2e, 0x00, 0x01, 0x01, player.Key, 0x00, 0x00, 0x00};


            if (player.Tcp.Connected)
                player.Tcp.GetStream().Write(packetKey, 0, packetKey.Length);
        }

        protected override AuthClient AuthServerConstructor()
        {
            return new AuthClient("Unogames", AuthClientTypeEnum.MessengerServer, 10111,
                "3493ef7ca4d69f54de682bee58be4f93");
        }
    }
}