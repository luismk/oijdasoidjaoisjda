using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PangyaAPI.Managers
{
    public class LobbyManager
    {
        public GenericDisposableCollection<Player> Players { get; set; }

        public GameManager GameManager { get; set; }

        private Channel _channel { get; set; }

        public LobbyManager(Channel channel)
        {
            _channel = channel;
            Players = new GenericDisposableCollection<Player>();
            GameManager = new GameManager(this);
        }

        /// <summary>
        /// Envia Lista para um player específico
        /// </summary>
        public void List(Player player)
        {
            //Se o player já existe
            if (Players.Model.Any(p => p.Member_Old.UID == player.Member_Old.UID))
            {
                Console.WriteLine("Player ja existe");
            }
            else
            {
                Players.Add(player);
            }

            SendToAll(PlayerAction(Players.ToList(), LobbyActionEnum.LIST));
        }

        public void Create(Player player)
        {
            SendToAll(PlayerAction(player, LobbyActionEnum.UPDATE));
        }

        public void Destroy(Player player)
        {
            Players.Model.Remove(player);

            SendToAll(PlayerAction(player, LobbyActionEnum.DESTROY));
        }

        public void Update(Player player)
        {
            SendToAll(PlayerAction(player, LobbyActionEnum.UPDATE));
        }

        private byte[] PlayerAction(List<Player> players, LobbyActionEnum action)
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0x46, 0x00 });
            result.Write((byte)action); //ACTION PLAYER
            result.Write((byte)players.Count); //TOTAL PLAYERS IN ACTION

            foreach (var player in players)
            {
                //----  lobbyPlayer.GetLobbyInfo()
                result.Write(player.Member_Old.UID);//4
                result.Write(player.ConnectionId);//8
                result.Write((ushort)0);//GameID
                result.WriteStr(player.Member_Old.Nickname, 22);//34
                result.Write((byte)player.User_Statistics.Game_Level);//35
                result.Write((uint)0); //GM(39)  (4 = GM, 0 = NormalPlayer);
                result.Write((uint)0);//43 //player.Decorations.TitleTypeID
                result.Write(0);//47
                result.Write((ushort)player.Member_Old.Sex); //[Sex] // Add $10 for wings  + $10 + $20(49)
                result.Write(player.Guild.GUILD_INDEX);
                result.WriteStr(player.Guild.GUILD_IMAGE, 9);
                result.WriteEmptyBytes(8);//70
                result.WriteStr(player.Member_Old.UsernameDomain, 18);//88
                result.WriteEmptyBytes(110);//198
                //---- END lobbyPlayer.GetLobbyInfo()
            }

            return result.GetBytes();
        }

        private byte[] PlayerAction(Player player, LobbyActionEnum action)
        {
            return PlayerAction(new List<Player>() { player }, action);
        }

        public void SendToAll(byte[] message)
        {
            Players.Model.ForEach(p => p.SendResponse(message));
        }

    }
}
