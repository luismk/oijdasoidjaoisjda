using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Models;
using PangyaAPI.Repository.Models;
using System;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_CREATE_GAME : HandleBase<Packet_PLAYER_CREATE_GAME>
    {
        #region Enums

        private enum CreateGameResultEnum
        {
            RESULT_SUCCESS = 0x00,
            RESULT_FULL = 0x02,
            ROOM_DONT_EXISTS = 0x03,
            INCORRECT_PASSWORD = 0x04,
            INVALID_LEVEL = 0x05,
            CREATE_FAILED = 0x07,
            ALREADY_STARTED = 0x08,
            CREATE_FAILED2 = 0x09,
            NEED_REGISTER_WITH_GUILD = 0x0D,
            PANG_BATTLE_INSSUFICENT_PANGS = 0x0F,
            APPROACH_INSSUFICENT_PANGS = 0x11,
            CANT_CREATE = 0x12
        };

        #endregion

        public Handle_PLAYER_CREATE_GAME(Player player) : base(player)
        {
            if (HandleCheckImplementedGameMode())
                Handle();
        }

        /// <summary>
        /// Verifica se o Modo de Game está implementado
        /// </summary>
        /// <returns></returns>
        private bool HandleCheckImplementedGameMode()
        {
            switch (PacketResult.Game.Mode)
            {
                case GameTypeEnum.TOURNEY:
                case GameTypeEnum.CHAT_ROOM:
                case GameTypeEnum.VERSUS_STROKE:
                    {
                        SendCreateResult(CreateGameResultEnum.RESULT_SUCCESS);
                        return true;
                    }
                default:
                    {
                        SendCreateResult(CreateGameResultEnum.CANT_CREATE);
                        return false;
                    }
            }
        }

        private void Handle()
        {
            PacketResult.Game.RoomKey = GameKey();
            PacketResult.Game.Owner_ID = Player.Member_Old.UID;

            var game = PacketResult.Game;

            Player.Game = game;

            Player.GameSlot = new GameSlot()
            {
                IsMaster = true,
                ReadyToPlay = true
            };

            game.Players.Add(Player);

            Player.Channel.Lobby.GameManager.Create(game);

            Player.SendResponse(GameInformation(game));

            switch (PacketResult.Game.Mode)
            {
                case GameTypeEnum.VERSUS_STROKE:
                    {
                        Player.SendResponse(game.CreateVS());
                    }
                    break;
                case GameTypeEnum.VERSUS_MATCH:
                    break;
                case GameTypeEnum.CHAT_ROOM:
                    {
                        Player.SendResponse(game.CreateChatMode(Player));
                    }
                    break;
                default:
                    Console.WriteLine("Game Mode Not Implemented: " + PacketResult.Game.Mode);
                    break;
            }

        }

        private byte[] GameKey()
        {
            var result = new byte[16];

            new Random().NextBytes(result);

            return result;
        }

        /// <summary>
        /// 49 00
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private byte[] GameInformation(Game game)
        {
            var result = new PangyaBinaryWriter();
            result.Write(new byte[] { 0x49, 0x00, 0x00, 0x00 });
            result.Write(game.GetGameInformation());
            return result.GetBytes();
        }

        private void SendCreateResult(CreateGameResultEnum result)
        {
            Player.SendResponse(new byte[] { 0x49, 0x00, (byte)result, 0x00 });
        }

    }
}
