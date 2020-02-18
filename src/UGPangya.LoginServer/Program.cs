using System;
using UGPangya.API;
using UGPangya.LoginServer.Handles;

namespace UGPangya.LoginServer
{
    public class Program
    {
        #region Fields

        public static LoginServerTcp _server;

        #endregion

        private static void Main(string[] args)
        {
            _server = new LoginServerTcp("149.56.33.81", 10103, 3000);
            _server.OnPacketReceived += TcpServer_OnPacketReceived;
            _server.ShowConsoleHelp();

            //Escuta contínuamente entradas no console (Criar comandos para o Console)
            for (;;)
            {
                var comando = Console.ReadLine().Split(new[] {' '}, 2);
                switch (comando[0].ToLower())
                {
                    case "": break;
                    case "help":
                        _server.ShowConsoleHelp();
                        break;
                    case "broadcast":
                        _server.BroadMessage(comando[1]);
                        break;
                    case "stop":
                        _server.IsOpen = false;
                        break;
                    case "start":
                        _server.IsOpen = true;
                        Console.WriteLine(DateTime.Now + " Server is Open");
                        break;

                    case "cls":
                    case "clear":
                    {
                        Console.Clear();
                    }
                        break;

                    case "quit":
                        Console.WriteLine("The server was stopped!");
                        Environment.Exit(1);
                        break;

                    default:
                        Console.WriteLine("Comando inexistente");
                        break;
                }
            }
        }

        /// <summary>
        ///     Servidor recebeu um packet
        /// </summary>
        /// <param name="player">Player que enviou o packet</param>
        /// <param name="packet">Informações do Packet</param>
        private static void TcpServer_OnPacketReceived(Player player, Packet packet)
        {
            #region Packets Handles

            Console.WriteLine(DateTime.Now + $" PACKET [{packet.Id}]: " + (PangyaPacketsEnum) packet.Id);

            switch ((PangyaPacketsEnum) packet.Id)
            {
                case PangyaPacketsEnum.PLAYER_LOGIN:
                {
                    new Handle_PLAYER_LOGIN(player);
                }
                    break;
                case PangyaPacketsEnum.PLAYER_SELECT_SERVER:
                {
                    new Handle_PLAYER_SELECT_SERVER(player);
                }
                    break;
                case PangyaPacketsEnum.PLAYER_DUPLICATE_LOGIN:
                {
                    //handle.HandleDuplicateLogin();
                }
                    break;
                case PangyaPacketsEnum.PLAYER_SET_NICKNAME:
                {
                    new Handle_PLAYER_SET_NICKNAME(player);
                }
                    break;
                case PangyaPacketsEnum.PLAYER_CONFIRM_NICKNAME:
                {
                    new Handle_PLAYER_CONFIRM_NICKNAME(player);
                }
                    break;
                case PangyaPacketsEnum.PLAYER_SELECT_CHARACTER:
                {
                    new Handle_PLAYER_SELECT_CHARACTER(player);
                }
                    break;
                case PangyaPacketsEnum.PLAYER_RECONNECT:
                {
                    new Handle_PLAYER_RECONNECT(player);
                    //handle.HandleReconnect();
                }
                    break;
                case PangyaPacketsEnum.NOTHING:
                    break;
                default:
                    Console.WriteLine("Packet id não identificado." + packet.Id);
                    break;
            }

            #endregion
        }
    }
}