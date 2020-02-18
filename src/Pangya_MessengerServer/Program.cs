using Pangya_MessengerServer.DataBase;
using PangyaAPI;
using System;

namespace Pangya_MessengerServer
{
    class Program
    {
        #region Fields 
        private static PangyaEntities _db;
        public static MessengerServerTcp _messengerserver;
        #endregion 
        static void Main(string[] args)
        {
            _db = new PangyaEntities();

            _messengerserver = new MessengerServerTcp(ip: "149.56.33.85", port: 30303, maxConnections: 3000);
            _messengerserver.OnClientConnected += (player => Console.WriteLine(DateTime.Now.ToString() + $" Player Conectado. Chave: [{Convert.ToString(player.Key)}]"));

            _messengerserver.OnPacketReceived += TcpServer_OnPacketReceived;

            //Escuta contínuamente entradas no console (Criar comandos para o Console)
            for (;;)
            {
                var comando = Console.ReadLine().Split(new char[] { ' ' }, 2);
                switch (comando[0].ToLower())
                {
                    case "": break;
                    default: Console.WriteLine("Comando inexistente"); break;
                }
            }
        }
        static void TcpServer_OnPacketReceived(Player playerType, Packet packet)
        {
            var player = (MPlayer)playerType;

            Console.WriteLine(DateTime.Now.ToString() + $" PACKET [{player.CurrentPacket.Id}]: " + ((MessengePacketEnum)player.CurrentPacket.Id));

            switch ((MessengePacketEnum)player.CurrentPacket.Id)
            {
                case MessengePacketEnum.PLAYER_LOGIN:
                    {
                        player.HandleClientReciveUID();
                    }
                    break;
                case MessengePacketEnum.PLAYER_SELECT_CHANNEL:
                    {
                        player.Test23();
                        //???
                        player.Test23_1();

                        //se conectou
                        player.Test23_2();
                    }
                    break;
                case MessengePacketEnum.UnIdentified_14:
                    {
                        player.RequestUnknown14();                     
                    }
                    break;
                case MessengePacketEnum.PLAYER_FIND_FRIEND:
                    {
                        player.PlayerSearchFriend();
                    }
                    break;
                case MessengePacketEnum.PLAYER_REQUEST_FRIEND:
                    {
                        player.RequestFriend();                        
                    }
                    break;
                case MessengePacketEnum.Unknown_19:
                    break;
                case MessengePacketEnum.Unknown_1D:
                    {
                        //sem packet ?
                    }
                    break;
                case MessengePacketEnum.PLAYER_CHAT:
                    break;
                case MessengePacketEnum.UnIdentified_16:
                    {
                        _messengerserver.DisconnectPlayer(player);
                    }
                    break;
                default:
                    Console.WriteLine($" Packet Not Found: [{player.CurrentPacket.Id}]");
                    break;
            }
        }       
    }
}