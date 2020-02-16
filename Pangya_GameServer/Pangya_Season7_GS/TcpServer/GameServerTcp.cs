using PangyaAPI;
using PangyaAPI.Auth;
using PangyaAPI.BinaryModels;
using PangyaAPI.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Linq;
using PangyaAPI.Repository.Models;

namespace Pangya_Season7_GS
{
    public class GameServerTcp : TcpServer
    {
        #region Public Fields

        public ServerFlag ServerFlag = ServerFlag.Default;

        public ServerProperty ServerProperty = ServerProperty.GP;

        public ChannelCollection Channels { get; set; }

        #endregion

        #region Construtor

        public GameServerTcp(string ip, int port, int maxConnections) : base(ip, port, maxConnections)
        {
            Channels = new ChannelCollection(this)
            {
                new Channel("Free #1", maxPlayers: 100, typechannel: ChannelTypeEnum.All, id: 1),
            };
        }

        #endregion

        #region Protected Overrrides Methods

        protected override void SendKey(Player player)
        {
            //Gera Packet com chave de criptografia (posisão 8)
            var US = new byte[] { 0x00, 0x06, 0x00, 0x00, 0x3f, 0x00, 0x01, 0x01, player.Key };

            var result = new PangyaBinaryWriter(new MemoryStream());

            result.Write(new byte[] { 0x00 });
            result.Write((UInt16)(player.GetIpAdress().Length + 8));
            result.Write(new byte[] { 0x00, 0x3F, 0x00, 0x01, 0x01 });
            result.Write(player.Key);
            result.Write(player.GetIpBytes());

            var TH = result.GetBytes();

            if (player.Tcp.Connected)
                player.SendBytes(US);
        }

        protected override AuthClient AuthServerConstructor()
        {
            return new AuthClient(name: "Unogames", type: AuthClientTypeEnum.GameServer, port: 30303, key: "3493ef7ca4d69f54de682bee58be4f93");
        }

        protected override void OnAuthServerPacketReceive(AuthClient client, AuthPacket packet)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region Public Overrrides Methods
        /// <summary>
        /// Usado somente no GameServer 
        /// </summary>
        /// <param name="player"></param>
        public override void DisconnectPlayer(Player player)
        {
            base.DisconnectPlayer(player);
        }
        #endregion
    }
}
