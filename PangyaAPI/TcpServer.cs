using PangyaAPI.Auth;
using PangyaAPI.BinaryModels;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PangyaAPI
{
    public abstract class TcpServer
    {
        #region Delegates

        public delegate void ConnectedEvent(Player player);
        public delegate void PacketReceivedEvent(Player player, Packet packet);

        #endregion

        #region Events
        /// <summary>
        /// Este evento ocorre quando o ProjectG se conecta ao Servidor
        /// </summary>
        public event ConnectedEvent OnClientConnected;

        /// <summary>
        /// Este evento ocorre quando o Servidor Recebe um Packet do ProjectG
        /// </summary>
        public event PacketReceivedEvent OnPacketReceived;

        #endregion

        #region Fields

        /// <summary>
        /// Define se o servidor está aberto para os jogadores
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Lista de Players conectados
        /// </summary>
        public GenericDisposableCollection<Player> Players = new GenericDisposableCollection<Player>();

        private int NextConnectionId { get; set; } = 0;

        private TcpListener _server;

        public AuthClient AuthServer;

        private bool _isRunning;

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Envia chave para o player
        /// </summary>
        protected abstract void SendKey(Player player);
        
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tcp"></param>
        //protected abstract Player CreatePlayer(TcpClient tcp);

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="ip">IP do servidor (Local ou Global)</param>
        /// <param name="port">Porta</param>
        /// <param name="maxConnections">
        /// Número máximo de conexões 
        /// Quando o Player se conecta ao Game-server, automaticamente ele é desconectado do LoginServer pois não necessita mais desta comunicação
        /// </param>
        public TcpServer(string ip, int port, int maxConnections)
        {
            try
            {
                //if (ConnectToAuthServer(AuthServerConstructor()) == false)
                //{
                //    Console.WriteLine("Não foi possível se conectar ao AuthServer");
                //    Console.ReadKey();
                //    Environment.Exit(1);
                //}

                //Abre servidor para os jogadores
                IsOpen = true;

                _server = new TcpListener(IPAddress.Parse(ip), port);

                //Inicia Servidor
                _server.Start(backlog: maxConnections);

                Console.WriteLine(DateTime.Now.ToString() + $" Servidor Iniciado na porta: {port}");

                _isRunning = true;

                //Inicia Thread para escuta de clientes
                var WaitConnectionsThread = new Thread(new ThreadStart(WaitConnections));
                WaitConnectionsThread.Start();
            }
            catch (Exception erro)
            {
                Console.WriteLine(DateTime.Now.ToString() + $" Erro ao iniciar o servidor: {erro.Message}");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Aguarda Conexões
        /// </summary>
        private void WaitConnections()
        {
            while (_isRunning)
            {
                // Inicia Escuta de novas conexões (Quando player se conecta).
                TcpClient newClient = _server.AcceptTcpClient();

                // Cliente conectado
                // Cria uma Thread para manusear a comunicação (uma thread por cliente)
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        /// <summary>
        /// Manuseia Comunicação do Cliente
        /// </summary>
        private void HandleClient(object obj)
        {
            //Recebe cliente a partir do parâmetro
            TcpClient tcpClient = (TcpClient)obj;

            //Cria novo player
            var player = new Player(tcpClient);

            this.Players.Add(player);

            //Define no player qual servidor ele está
            player.Server = this;

            ////Chama método que faz chamada ao evento OnClientConnected
            PlayerConnected(player);

            NetworkStream clientStream = player.Tcp.GetStream();

            //Escuta contínuamente as mensagens do player (ProjectG) enquanto estiver conectado
            while (player.Tcp.Connected)
            {
                try
                {
                    var messageBufferRead = new byte[500000]; //Tamanho do BUFFER á ler

                    //Lê mensagem do cliente
                    int bytesRead = clientStream.Read(messageBufferRead, 0, 500000);

                    if(bytesRead == 0 && player.DuplicatedLogin)
                    {
                        Console.WriteLine("teste reconnect");
                        SendKey(player);
                        continue;
                    }

                    //variável para armazenar a mensagem recebida
                    byte[] message = new byte[bytesRead];

                    //Copia mensagem recebida
                    Buffer.BlockCopy(messageBufferRead, 0, message, 0, bytesRead);

                    if (message.Length >= 5)
                    {
                        if (player.CurrentPacket != null)
                            player.PreviousPacket = new Packet(player.CurrentPacket.MessageCrypted, player.Key);

                        player.CurrentPacket = new Packet(message, player.Key);
                        
                        //Dispara evento OnPacketReceived
                        OnPacketReceived?.Invoke(player, packet: player.CurrentPacket);
                    }
                    else
                    {
                        //Sem Resposta
                        DisconnectPlayer(player);
                    }
                }
                catch (Exception erro)
                {
                    System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(erro, true);

                    Console.WriteLine("[Exception] " + erro.Message);

                    Console.WriteLine(trace.GetFrame(0).GetMethod().ReflectedType.FullName);
                    Console.WriteLine("Method: " + erro.TargetSite);
                    Console.WriteLine("Line: " + trace.GetFrame(0).GetFileLineNumber());
                    Console.WriteLine("Column: " + trace.GetFrame(0).GetFileColumnNumber());


                    //Desconecta player
                    DisconnectPlayer(player);
                }
            }

            //Caso o player não estiver mais conectado
            DisconnectPlayer(player);
        }

        private void PlayerConnected(Player player)
        {
            player.ConnectionId = NextConnectionId;

            NextConnectionId += 1;

            Console.WriteLine(DateTime.Now.ToString() + $" Player Connected. Total Players Connected:  {Players.Model.Count}");

            SendKey(player);

            //Chama evento OnClientConnected
            OnClientConnected?.Invoke(player);
        }

        #endregion

        #region Public Methods

        public virtual void DisconnectPlayer(Player player)
        {
            player.Dispose();
        }

        public void SendToAll(byte[] Data)
        {
            //for (int i = 0; i < this.Players.ToList().Count; i++)
            //{
            //    Players[i].SendResponse(Data);
            //}
        }

        #endregion

        #region Comandos no console

        public void BroadMessage(string message)
        {
            var response = new PangyaBinaryWriter(new MemoryStream());

            response.Write(new byte[] { 0x42, 0x00 });
            response.WritePStr("Aviso: " + message);

            SendToAll(response.GetBytes());
            Console.WriteLine("BroadCast enviado com sucesso");
        }

        public void TickerMessage(string message)
        {
            var response = new PangyaBinaryWriter(new MemoryStream());

            response.Write(new byte[] { 0xC9, 0x00 });
            response.WritePStr("@Admin");
            response.WritePStr(message);
            response.WriteEmptyBytes(1);

            SendToAll(response.GetBytes());

            Console.WriteLine("Ticker enviado com sucesso");
        }

        public void ServerMessage(string message)
        {
            var response = new PangyaBinaryWriter(new MemoryStream());

            response.Write(new byte[] { 0x43, 0x00 });
            response.WritePStr(message);

            SendToAll(response.GetBytes());

            Console.WriteLine("Mensagem enviada com sucesso");
        }

        #endregion

        #region AuthServer

        protected abstract AuthClient AuthServerConstructor();

        protected abstract void OnAuthServerPacketReceive(AuthClient client, AuthPacket packet);


        /// <summary>
        /// Conecta-se ao AuthServer
        /// </summary>
        private bool ConnectToAuthServer(AuthClient client)
        {
            AuthServer = client;
            AuthServer.OnDisconnect += OnAuthServerDisconnected;
            AuthServer.OnPacketReceived += AuthServer_OnPacketReceived;
            return AuthServer.Connect();
        }

        /// <summary>
        /// É Disparado quando um packet é recebido do AuthServer
        /// </summary>
        private void AuthServer_OnPacketReceived(AuthClient authClient, AuthPacket packet)
        {
            OnAuthServerPacketReceive(authClient, packet);
        }

        /// <summary>
        /// É disparado quando não há conexão com o AuthServer
        /// </summary>
        private void OnAuthServerDisconnected()
        {
            Console.WriteLine("Servidor parado.");
            Console.WriteLine("Não foi possível estabelecer conexão com o authServer!");
            Console.ReadKey();
            Environment.Exit(1);
        }

        #endregion
    }
}
