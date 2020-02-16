using PangyaAPI.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository.Models
{
    public class Channel
    {
        #region Public Fields
        public GenericDisposableCollection<Player> Players { get; set; }

        public LobbyManager Lobby { get; set; }

        //public GameCollection Games { get; set; }

        //public LobbyPlayerManager LobbyPlayers { get; set; }

        public byte Id { get; set; }

        public string Name { get; set; }

        public ushort MaxPlayers { get; set; }

        public ChannelTypeEnum Flag { get; set; }
        #endregion

        #region Construtor

        public Channel(string name, ushort maxPlayers, ChannelTypeEnum typechannel, byte id)
        {
            Name = name;
            MaxPlayers = maxPlayers;
            Id = id;
            Flag = typechannel;

            //Games = new GameCollection(this);
            Players = new GenericDisposableCollection<Player>();
            Lobby = new LobbyManager(this);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Verifica se o Canal está cheio
        /// </summary>
        public bool IsFull()
        {
            return Players.Model.Count >= MaxPlayers;
        }

        public void PlayerJoin(Player player)
        {
            player.Channel = this;

            Players.Add(player);
        }

        #endregion
    }
}
