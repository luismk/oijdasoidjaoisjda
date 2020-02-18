using Pangya_Season7_GS.Handles;
using PangyaAPI;
using System;

namespace Pangya_Season7_GS
{
    class Program
    {
        #region Fields 

        static GameServerTcp _gameserver;

        #endregion

        static void Main(string[] args)
        {
            Console.Title = "Pangya Fresh UP ! GameServer";

            //Inicia servidor
            _gameserver = new GameServerTcp(ip: "149.56.33.81", port: 20201, maxConnections: 3000);

            _gameserver.OnClientConnected += (player =>
            {
                player.Server = _gameserver;
                Console.WriteLine(DateTime.Now.ToString() + $" Player Conectado. Chave: [{Convert.ToString(player.Key)}]");
            });

            _gameserver.OnPacketReceived += TcpServer_OnPacketReceived;

            ////Escuta contínuamente entradas no console (Criar comandos para o Console)
            //for (; ; )
            //{
            //    var comando = Console.ReadLine().Split(new char[] { ' ' }, 2);

            //    switch (comando[0].ToLower())
            //    {
            //        case "": break;
            //        case "broad": _gameserver.BroadMessage(comando[1]); break;
            //        case "msgtk": _gameserver.TickerMessage(comando[1]); break;
            //        default: Console.WriteLine("Comando inexistente"); break;
            //    }
            //}
        }

        static void TcpServer_OnPacketReceived(Player player, Packet packet)
        {
            var msgPacket = DateTime.Now.ToString() + $" PACKET [{player.CurrentPacket.Id}]: " + ((GamePacketEnum)player.CurrentPacket.Id);

            Console.WriteLine(msgPacket);

            switch ((GamePacketEnum)player.CurrentPacket.Id)
            {
                #region OK
                case GamePacketEnum.PLAYER_LOGIN:
                    {
                        new Handle_PLAYER_LOGIN(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_SELECT_LOBBY:
                    {
                        new Handle_PLAYER_SELECT_LOBBY(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_SAVE_BAR:
                    {
                        new Handle_PLAYER_SAVE_BAR(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_CHANGE_EQUIPMENTS:
                    {
                        new Handle_PLAYER_CHANGE_EQUIPMENTS(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_JOIN_MULTIGAME_LIST:
                    {
                        new Handle_PLAYER_JOIN_MULTIGAME_LIST(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_LEAVE_MULTIGAME_LIST:
                    {
                        new Handle_PLAYER_LEAVE_MULTIGAME_LIST(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_CHAT:
                    {
                        new Handle_PLAYER_CHAT(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_ENTER_TO_SHOP:
                    {
                        new Handle_PLAYER_ENTER_TO_SHOP(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_BUY_ITEM_GAME:
                    {
                        new Handle_PLAYER_BUY_ITEM_GAME(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_CREATE_GAME:
                    {
                        new Handle_PLAYER_CREATE_GAME(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_LEAVE_GAME:
                    {
                        new Handle_PLAYER_LEAVE_GAME(player);
                    }
                    break;
                #endregion
                case GamePacketEnum.PLAYER_REQUEST_PLAYERINFO:
                    {
                        new Handle_PLAYER_REQUEST_PLAYERINFO(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_JOIN_GAME:
                    {
                        new Handle_PLAYER_JOIN_GAME(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_PRESS_READY:
                    {
                        new Handle_PLAYER_PRESS_READY(player);
                    }
                    break;
                    //Start Game
                case GamePacketEnum.PLAYER_START_GAME:
                    {
                        new Handle_PLAYER_START_GAME(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_LOADING_INFO:
                    {
                        new Handle_PLAYER_LOADING_INFO(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_CHANGE_EQUIPMENT:
                    {
                        new Handle_PLAYER_CHANGE_EQUIPMENT(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_HOLE_INFORMATIONS:
                    {
                        new Handle_PLAYER_HOLE_INFORMATIONS(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_1ST_SHOT_READY:
                    {
                       new Handle_PLAYER_1ST_SHOT_READY(player);
                    }
                    break;

                case GamePacketEnum.PLAYER_LOAD_OK:
                    {
                        new Handle_PLAYER_LOAD_OK(player);
                    }
                    break;

                case GamePacketEnum.PLAYER_MATCH_DATA:
                    {
                        new Handle_PLAYER_MATCH_DATA(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_SHOT_DATA:
                    {
                        new Handle_PLAYER_SHOT_DATA(player);
                    }
                    break;

                //TESTE VS
                case GamePacketEnum.PLAYER_MATCH_HISTORY:
                    {
                        //new Handle_PLAYER_MATCH_HISTORY(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_SHOT_SYNC:
                    {
                        new Handle_PLAYER_SHOT_SYNC(player);
                    }
                    break;

                case GamePacketEnum.PLAYER_ACTION:
                    {
                        new Handle_PLAYER_ACTION(player);
                    }
                    break;

                case GamePacketEnum.PLAYER_GM_COMMAND:
                    {
                        new Handle_PLAYER_GM_COMMAND(player);
                    }
                    break;

                #region GAMEPLAY

                case GamePacketEnum.PLAYER_ACTION_SHOT:
                    {
                        new Handle_PLAYER_ACTION_SHOT(player);
                    }
                    break;

                case GamePacketEnum.PLAYER_GAME_ROTATE:
                    {
                        new Handle_PLAYER_GAME_ROTATE(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_PAUSE_GAME:
                    {
                        new Handle_PLAYER_PAUSE_GAME(player);
                    }
                    break;
                #endregion


                #region GUILD
                case GamePacketEnum.PLAYER_GUILD_AVAIABLE:
                    {
                        new Handle_PLAYER_GUILD_AVALIABLE(player);
                    }
                    break;
                case GamePacketEnum.PLAYER_CREATE_GUILD:
                    {
                        new Handle_PLAYER_CREATE_GUILD(player);
                    }
                    break;
                    #endregion

            }
        }
    }
}
