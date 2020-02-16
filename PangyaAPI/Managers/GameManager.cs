using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace PangyaAPI.Managers
{
    public class GameManager
    {
        public List<Game> Games { get; set; }

        private LobbyManager _lobby { get; set; }

        public GameManager(LobbyManager lobby)
        {
            _lobby = lobby;

            Games = new List<Game>();
        }

        public void List(Player player)
        {
            player.SendResponse(GameAction(Games, GameActionEnum.LIST));
        }

        /// <summary>
        /// 47 00
        /// </summary>
        /// <param name="game"></param>
        public void Create(Game game)
        {
            #region Gera ROOM_NUMBER
            ushort room_number = 2;
            while (Games.Any(g => g.GameId == room_number))
            {
                room_number += 1;
            }

            //Obtém um Room_Number ainda não utilizado
            game.GameId = room_number;

            //Atualiza room_number do player
            //game.Players.GetAll().First().GameID = room_number;

            #endregion

            //Adiciona GAME
            Games.Add(game);

            _lobby.Players.ToList().ForEach(player =>
            {
                player.SendResponse(GameAction(game, GameActionEnum.CREATE));
            });
        }

        public void Destroy(Game game)
        {
            _lobby.Players.ToList().ForEach(player =>
            {
                player.SendResponse(GameAction(game, GameActionEnum.UPDATE)); //Update para exibir aos jogadores a contagem 0
                player.SendResponse(GameAction(game, GameActionEnum.DESTROY)); //Destroy
            });

            Games.Remove(game);
        }

        /// <summary>
        /// 47 00
        /// </summary>
        /// <param name="game"></param>
        public void Update(Game game)
        {
            _lobby.SendToAll(GameAction(game, GameActionEnum.UPDATE));
        }

        private byte[] GameAction(List<Game> games, GameActionEnum action)
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[] {
                0x47, 0x00,
                (byte)games.Count,
                (byte)action,
                0xFF, 0xFF,
            });

            games.ForEach(game =>
            {
                result.Write(game.GetGameInformation());
            });

            return result.GetBytes();
        }

        private byte[] GameAction(Game game, GameActionEnum action)
        {
            return GameAction(new List<Game>() { game }, action);
        }

    }
}
