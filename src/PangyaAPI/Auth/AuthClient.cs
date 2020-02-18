using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace PangyaAPI.Auth
{
    public class AuthClient : IDisposeable
    {
        #region Delegates
        public delegate void DisconnectedEvent();
        public delegate void PacketReceivedEvent(AuthClient authClient, AuthPacket packet);
        #endregion

        #region Events

        /// <summary>
        /// Este evento ocorre quando o ProjectG se conecta ao Servidor
        /// </summary>
        public event DisconnectedEvent OnDisconnect;

        /// <summary>
        /// Este evento ocorre quando o client recebe um Packet do AuthServer
        /// </summary>
        public event PacketReceivedEvent OnPacketReceived;
        #endregion

        #region Public Fields

        public string Key { get; set; }

        public string Name { get; set; }

        public AuthClientTypeEnum Type { get; set; }

        public int Port { get; set; }

        //Conexão
        public TcpClient Tcp;

        public AuthPacket CurrentPacket { get; set; }

        #endregion

        #region Constructor

        public AuthClient(string name, AuthClientTypeEnum type, int port, string key)
        {
            Tcp = new TcpClient();
            this.Name = name;
            this.Type = type;
            this.Port = port;
            this.Key = key;
        }

        public AuthClient(TcpClient client)
        {
            Tcp = client;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Verifica de tempo em tempo se o AuthServer ainda está conectado.
        /// </summary>
        private void KeepAlive()
        {
            while (true)
            {
                //Aguarda tempo
                Thread.Sleep(TimeSpan.FromSeconds(5));

                try
                {
                    //Send KeepAlive
                    Send(new AuthPacket()
                    {
                        Id = AuthPacketEnum.SERVER_KEEPALIVE
                    });
                }
                catch
                {
                    //Dispara evento quando não há conexão
                    OnDisconnect?.Invoke();
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Conecta-se ao AuthServer
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            Tcp.Connect("127.0.0.1", Port);

            var packet = new AuthPacket()
            {
                Id = AuthPacketEnum.SERVER_CONNECT,
                Message = new
                {
                    ServerName = Name,
                    ServerType = Type,
                    Key
                },
            };

            var response = SendAndReceive(packet);

            if (response.Message.Success == true)
            {
                //Inicia Thread KeepAlive
                var authClienthread = new Thread(new ThreadStart(HandleAuthClient));
                authClienthread.Start();

                Console.WriteLine("Autenticação AuthServer realizada com sucesso!");

                return true;
            }
            else
            {
                Console.WriteLine(response.Message.Exception);
                return false;
            }
        }

        private void HandleAuthClient()
        {
            //Inicia Thread KeepAlive
            var keepAliveThread = new Thread(new ThreadStart(KeepAlive));
            keepAliveThread.Start();

            while (Tcp.Connected)
            {
                try
                {
                    var messageBufferRead = new byte[500000]; //Tamanho do BUFFER á ler

                    //Lê mensagem do cliente
                    var bytesRead = Tcp.GetStream().Read(messageBufferRead, 0, 500000);

                    //variável para armazenar a mensagem recebida
                    var message = new byte[bytesRead];

                    //Copia mensagem recebida
                    Buffer.BlockCopy(messageBufferRead, 0, message, 0, bytesRead);

                    var json = System.Text.Encoding.Default.GetString(message);

                    var response = JsonConvert.DeserializeObject<AuthPacket>(json);

                    //Dispara evento OnPacketReceived
                    OnPacketReceived?.Invoke(this, response);
                }
                catch
                {
                    OnDisconnect?.Invoke();
                }
            }
        }

        /// <summary>
        /// Envia Packet sem aguardar uma Resposta
        /// </summary>
        public void Send(AuthPacket packet)
        {
            var _stream = Tcp.GetStream();

            var json = JsonConvert.SerializeObject(packet);

            var result = json.Select(Convert.ToByte).ToArray();
            _stream.Write(result, 0, result.Length);
        }

        /// <summary>
        /// Envia Packet aguardando uma resposta
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public AuthPacket SendAndReceive(AuthPacket packet)
        {
            Send(packet);

            var messageBufferRead = new byte[500000]; //Tamanho do BUFFER á ler

            //Lê mensagem do cliente
            var bytesRead = Tcp.GetStream().Read(messageBufferRead, 0, 500000);

            //variável para armazenar a mensagem recebida
            var message = new byte[bytesRead];

            //Copia mensagem recebida
            Buffer.BlockCopy(messageBufferRead, 0, message, 0, bytesRead);

            var json = System.Text.Encoding.Default.GetString(message);

            var response = JsonConvert.DeserializeObject<AuthPacket>(json);

            return response;
        }

        #endregion

        #region IDisposable
        // booleano para controlar se
        // o método Dispose já foi chamado
        public bool Disposed { get; set; }

        // método privado para controle
        // da liberação dos recursos
        private void Dispose(bool disposing)
        {
            // Verifique se Dispose já foi chamado.
            if (!this.Disposed)
            {
                if (disposing)
                {
                    // Liberando recursos gerenciados
                    Tcp.Dispose();
                }

                // Seta a variável booleana para true,
                // indicando que os recursos já foram liberados
                Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // C#
        ~AuthClient()
        {
            Dispose(false);
        }
        #endregion
    }

   
}
