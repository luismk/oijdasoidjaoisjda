using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Models;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_JOIN_GAME : HandleBase<Packet_PLAYER_JOIN_GAME>
    {
        public Handle_PLAYER_JOIN_GAME(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var game = Player.Channel.Lobby.GameManager.Games.FirstOrDefault(g => g.GameId == PacketResult.RoomNumber);

            Player.GameSlot = new GameSlot();
            Player.Game = game;

            game.Players.Add(Player);

            Player.SendResponse(GameInformation(game));

            switch (game.Mode)
            {
                case GameTypeEnum.VERSUS_STROKE:
                    {
                        game.Players.ForEach(p => p.SendResponse(game.CreateVS()));
                    }
                    break;
                case GameTypeEnum.CHAT_ROOM:
                    {
                        game.CreateChatModeList(Player);
                        game.Players.ForEach(p => p.SendResponse(game.CreateChatMode(Player)));
                    }
                    break;
            }

        }

        private byte[] GameInformation(Game game)
        {
            var result = new PangyaBinaryWriter();
            result.Write(new byte[] { 0x49, 0x00, 0x00, 0x00 });
            result.Write(game.GetGameInformation());
            return result.GetBytes();
        }

        //private byte[] CreateVS()
        //{
        //    var result = new PangyaBinaryWriter();

        //    result.Write(new byte[]
        //    {
        //        0x48, 0x00,0x00,
        //        0xFF, 0xFF,
        //        0x01 //PlayersCount
        //    });
        //    result.Write(Player.ConnectionId);

        //    result.Write(Player.GetGameInfo(0x01));

        //    result.Write(Player.Characters.GetCharacterData(Player.Characters.First().CID)); //result.Write(Inventory.GetCharData());
        //    result.Write((byte)0);

        //    return result.GetBytes();
        //}
    }
}
