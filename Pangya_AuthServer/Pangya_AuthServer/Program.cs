using PangyaAPI.Auth;
using System;

namespace Pangya_AuthServer
{
    class Program
    {
        public static AuthServer _authServer;

        public static string _authKey { get; set; }

        static void Main(string[] args)
        {
            _authKey = "3493ef7ca4d69f54de682bee58be4f93"; //Unogames em MD5

            //Inicia servidor
            _authServer = new AuthServer(ip: "127.0.0.1", port: 30303, maxConnections: 3000);
            _authServer.OnPacketReceived += TcpServer_OnPacketReceived;
        }

        static void TcpServer_OnPacketReceived(AuthClient client, AuthPacket packet)
        {
            Console.WriteLine(DateTime.Now.ToString() + $" PACKET [{packet.Id}]");

            switch (packet.Id)
            {
                case AuthPacketEnum.SERVER_CONNECT:
                    {
                        var response = new AuthPacket();

                        //Verifica se achave AuthKey está correta
                        if (client.Key == _authKey)
                        {
                            response.Message = new
                            {
                                Success = true,
                            };

                            _authServer.Send(client, response);

                            _authServer.ShowConnectedClients();
                        }
                        else
                        {
                            response.Message = new
                            {
                                Success = false,
                                Exception = "Chave de autenticação inválida"
                            };

                            _authServer.Send(client, response);
                            _authServer.DisconnectClient(client);
                        }
                    }
                    break;
                case AuthPacketEnum.LS_PLAYER_DUPLCATE_LOGIN:
                    {
                        _authServer.SendToAll(packet); //Envia para todos o packet DuplicateLogin
                    }
                    break;
                case AuthPacketEnum.SERVER_KEEPALIVE:
                    {
                        _authServer.Send(client, new AuthPacket() { Id = 0x00 });
                    }
                    break;
                default:
                    Console.WriteLine("Packet ID inválido: " + packet.Id);
                    break;
            }
        }

        public static void ServerList()
        {

        }
    }
}
