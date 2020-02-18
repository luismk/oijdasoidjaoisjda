using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public class Packet_PLAYER_LOGIN : PacketResult
    {
        /// <summary>
        ///     Nome do usuário logado
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Identificador do usuário
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        ///     Chave de Autenticação do LoginServer
        /// </summary>
        public string AuthKeyLogin { get; set; }

        /// <summary>
        ///     Chave de Autenticação do GameServer
        /// </summary>
        public string AuthKeyGame { get; set; }

        /// <summary>
        ///     Versão do ProjectG
        /// </summary>
        public string ServerVersion { get; set; }

        /// <summary>
        ///     ???
        /// </summary>
        public uint Check { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Username = reader.ReadPStr();
            UID = reader.ReadInt32();
            reader.Skip(6);
            AuthKeyLogin = reader.ReadPStr();
            ServerVersion = reader.ReadPStr();
            Check = reader.ReadUInt32();
            reader.Skip(4);
            AuthKeyGame = reader.ReadPStr();
        }
    }
}