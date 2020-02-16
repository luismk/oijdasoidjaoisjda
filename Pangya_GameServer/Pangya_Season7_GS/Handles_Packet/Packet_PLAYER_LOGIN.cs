using PangyaAPI;
using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_LOGIN : PacketResult
    {
        /// <summary>
        /// Nome do usuário logado
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Identificador do usuário
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// Chave de Autenticação do LoginServer
        /// </summary>
        public string AuthKeyLogin { get; set; }

        /// <summary>
        /// Chave de Autenticação do GameServer
        /// </summary>
        public string AuthKeyGame { get; set; }

        /// <summary>
        /// Versão do ProjectG
        /// </summary>
        public string ServerVersion { get; set; }

        /// <summary>
        /// ???
        /// </summary>
        public uint Check { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            this.Username = reader.ReadPStr();
            this.UID = reader.ReadInt32();
            reader.Skip(6);
            this.AuthKeyLogin = reader.ReadPStr();
            this.ServerVersion = reader.ReadPStr();
            this.Check = reader.ReadUInt32();
            reader.Skip(4);
            this.AuthKeyGame = reader.ReadPStr();
        }
    }
}
