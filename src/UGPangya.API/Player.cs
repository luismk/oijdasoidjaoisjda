using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UGPangya.API.BinaryModels;
using UGPangya.API.Collections;
using UGPangya.API.Models;
using UGPangya.API.Repository;
using UGPangya.API.Repository.Models;
using UGPangya.Connector.Repository.Model;

namespace UGPangya.API
{
    public partial class Player : IDisposeable
    {
        #region Delegates

        public delegate void PacketChangedEvent();

        #endregion

        private readonly string _connectionString;

        public ChatGameInfo ChatGameInfo = new ChatGameInfo();

        public PlayerPosition Position = new PlayerPosition();

        #region Constructor

        public Player(TcpClient tcp)
        {
            _connectionString = Settings.Default.ConnectionString;

            Tcp = tcp;

            //Gera uma chave dinâmica
            Key = Convert.ToByte(new Random().Next(1, 17));

            //Maximo hexadecimal value: FF (255)

            ////Chave Fixa
            Key = 0x0A; //chave 10

            //_responseStream = new MemoryStream();
            Response = new PangyaBinaryWriter(new MemoryStream());
        }

        #endregion

        public HolePos HolePos { get; set; }

        #region Events

        public event PacketChangedEvent OnPacketChanged;

        #endregion

        #region Repositories

        private readonly MemberRepository _pangyaMemberRepository = new MemberRepository();
        private readonly UserEquipRepository _pangyaUserEquipRepository = new UserEquipRepository();
        private readonly GameMacroRepository _pangyaGameMacroRepository = new GameMacroRepository();
        private readonly UserStatisticsRepository _pangyaUserStatisticsRepository = new UserStatisticsRepository();
        private readonly GuildRepository _pangyaGuildRepository = new GuildRepository();
        private CharacterRepository _characterRepository = new CharacterRepository();
        private CaddieRepository _caddieRepository = new CaddieRepository();

        #endregion

        #region Public Fields

        public bool DuplicatedLogin { get; set; }

        /// <summary>
        ///     Servidor em que o cliente está conectado
        /// </summary>
        public TcpServer Server { get; set; }

        /// <summary>
        ///     Canal
        /// </summary>
        public Channel Channel { get; set; }

        public Game Game { get; set; }

        /// <summary>
        ///     Conexão do cliente
        /// </summary>
        public TcpClient Tcp { get; set; }

        /// <summary>
        ///     Chave de criptografia e decriptografia
        /// </summary>
        public byte Key { get; }

        public PangyaBinaryWriter Response { get; set; }

        /// <summary>
        ///     Packet Atual
        /// </summary>
        private Packet _currentPacket;

        /// <summary>
        ///     Packet Atual
        /// </summary>
        public Packet CurrentPacket
        {
            get => _currentPacket;
            set
            {
                _currentPacket = value;
                OnPacketChanged?.Invoke();
            }
        }

        /// <summary>
        ///     Packet Anterior
        /// </summary>
        public Packet PreviousPacket { get; set; }

        /// <summary>
        ///     Identificador da conexão
        /// </summary>
        public int ConnectionId { get; set; }

        ///// <summary>
        ///// Dados do player na Guild
        ///// </summary>
        //public PlayerMemberGuild GuildData { get; set; }
        /// <summary>
        ///     dados do record do player
        /// </summary>
        public PlayerMemberRecordInfo RecordData { get; set; }

        /// <summary>
        ///     obtem dados do macro do player F1 F2 F3
        /// </summary>

        #endregion

        #region Identity Fields

        //public string Username { get; set; }

        public Member Member_Old { get; set; }

        public MemberModel Member { get; set; }

        public UserStatistics User_Statistics { get; set; }

        public UserEquip UserEquip { get; set; }

        public Guild Guild { get; set; }

        public CharacterCollection Characters { get; set; }

        public CaddieCollection Caddies { get; set; }

        public MascotCollection Mascots { get; set; }

        public WarehouseCollection WarehouseCollection { get; set; }

        #endregion

        #region Public Methods

        public string GetIpAdress()
        {
            var ip = ((IPEndPoint) Tcp.Client.RemoteEndPoint).Address.ToString();
            return ip;
        }

        public byte[] GetIpBytes()
        {
            var getip = new byte[GetIpAdress().Length];
            return getip;
        }

        public void Disconnect()
        {
            Server.DisconnectPlayer(this);
        }

        #endregion

        #region Dispose

        // booleano para controlar se
        // o método Dispose já foi chamado
        public bool Disposed { get; set; }

        // método privado para controle
        // da liberação dos recursos
        private void Dispose(bool disposing)
        {
            // Verifique se Dispose já foi chamado.
            if (!Disposed)
            {
                if (disposing)
                    // Liberando recursos gerenciados
                    Tcp.Dispose();

                // Seta a variável booleana para true,
                // indicando que os recursos já foram liberados
                Disposed = true;
            }
        }

        public void Dispose()
        {
            if (Tcp.Connected)
                Tcp.Close();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Destrutor
        /// </summary>
        ~Player()
        {
            Dispose(false);
        }

        #endregion
    }
}