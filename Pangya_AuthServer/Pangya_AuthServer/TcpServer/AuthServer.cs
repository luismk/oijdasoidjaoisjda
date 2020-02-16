using Newtonsoft.Json;
using PangyaAPI;
using PangyaAPI.Auth;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Pangya_AuthServer
{
    public class AuthServer
    {
        #region Delegates
        public delegate void ConnectedEvent(AuthClient client);
        public delegate void PacketReceivedEvent(AuthClient client, AuthPacket packet);
        #endregion

        #region Events

        /// <summary>
        /// Este evento ocorre quando um Servidor se conecta com o Authserver
        /// </summary>
        public event ConnectedEvent OnClientConnected;

        /// <summary>
        /// Este evento ocorre quando o AuthServer Recebe um Packet de um Servidor
        /// </summary>
        public event PacketReceivedEvent OnPacketReceived;

        #endregion

        #region Fields

        /// <summary>
        /// Lista de Clientes conectados
        /// </summary>
        public GenericDisposableCollection<AuthClient> Clients = new GenericDisposableCollection<AuthClient>();

        private TcpListener _server;

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
        public AuthServer(string ip, int port, int maxConnections)
        {
            try
            {
                _server = new TcpListener(IPAddress.Parse(ip), port);

                //Inicia Servidor
                _server.Start(backlog: maxConnections);

                Console.WriteLine(DateTime.Now.ToString() + $" Servidor Iniciado na porta: {port}");

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
            while (true)
            {
                // Inicia Escuta de novas conexões.
                TcpClient newClient = _server.AcceptTcpClient();

                // Cliente conectado
                // Cria uma Thread para manusear a comunicação
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        /// <summary>
        /// Manuseia Comunicação do Cliente
        /// </summary>
        private void HandleClient(object obj)
        {
            try
            {
                //Recebe cliente a partir do parâmetro
                TcpClient tcpClient = (TcpClient)obj;

                NetworkStream clientStream = tcpClient.GetStream();

                #region READ ON CONNECT INICIAL

                AuthPacket packet = ReceivePacket(clientStream);

                var client = new AuthClient(tcpClient)
                {
                    Name = packet.Message.ServerName,
                    Type = packet.Message.ServerType,
                    Key = packet.Message.Key
                };

                Clients.Add(client);

                ClientConnected(client);

                OnPacketReceived?.Invoke(client, packet);

                #endregion

                //Escuta contínuamente as mensagens dos clientes (Servidores) enquanto estiver conectado
                while (tcpClient.Connected)
                {
                    try
                    {
                        packet = ReceivePacket(clientStream);

                        OnPacketReceived?.Invoke(client, packet);
                    }
                    catch (Exception erro)
                    {
                        Console.WriteLine(DateTime.Now.ToString() + $" Exception error:" + Environment.NewLine);
                        Console.WriteLine(erro.Message + Environment.NewLine);

                        //Desconecta client
                        DisconnectClient(client);
                    }
                }

                //Caso o Client não estiver mais conectado
                DisconnectClient(client);
            }
            catch (Exception erro)
            {
                Console.WriteLine(DateTime.Now.ToString() + $" Exception error:" + Environment.NewLine);
                Console.WriteLine(erro.Message + Environment.NewLine);
            }
        }

        private static AuthPacket ReceivePacket(NetworkStream clientStream)
        {
            var messageBufferRead = new byte[500000]; //Tamanho do BUFFER á ler

            //Lê mensagem do cliente
            var bytesRead = clientStream.Read(messageBufferRead, 0, 500000);

            //variável para armazenar a mensagem recebida
            var message = new byte[bytesRead];

            //Copia mensagem recebida
            Buffer.BlockCopy(messageBufferRead, 0, message, 0, bytesRead);

            var json = System.Text.Encoding.Default.GetString(message);

            var packet = JsonConvert.DeserializeObject<AuthPacket>(json);
            return packet;
        }

        private void ClientConnected(AuthClient client)
        {
            //Chama evento OnClientConnected
            OnClientConnected?.Invoke(client);

            Console.WriteLine($"Server: {client.Name} | Type: {client.Type} Connected");
        }
        #endregion

        #region Public Methods

        public virtual void DisconnectClient(AuthClient client)
        {
            client.Dispose();

            Console.WriteLine(DateTime.Now.ToString() + $" Server Disconnected: <{client.Name}>");
        }

        public void SendToAll(AuthPacket packet)
        {
            for (int i = 0; i < this.Clients.ToList().Count; i++)
            {
                Send(Clients[i], packet);
            }
        }

        public void Send(AuthClient client, AuthPacket packet)
        {
            var _stream = client.Tcp.GetStream();

            var json = JsonConvert.SerializeObject(packet);

            var result = json.Select(Convert.ToByte).ToArray();
            _stream.Write(result, 0, result.Length);
        }

        public void ShowConnectedClients()
        {
            Console.WriteLine("Connected Clients:" + Environment.NewLine);

            var clients = Clients.ToList().OrderBy(c => c.Type);
            clients.ToList().ForEach(client =>
             {
                 Console.WriteLine($"Type: {client.Type} | Name: {client.Name}");
             });
        }

        #endregion

    }
}
