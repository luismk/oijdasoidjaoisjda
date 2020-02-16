using PangyaCryptography;
using PangyaAPI.BinaryModels;
using System.Collections.Generic;
using System.IO;

namespace PangyaAPI
{
    public partial class Player
    {

        public void Send(byte[] message)
        {
            var buffer = ServerCipher.Encrypt(message, Key, 0);

            SendBytes(buffer);
        }

        //Envia Resposta através do PangyaBinaryWriter
        public void SendResponse()
        {
            Send(Response.GetBytes());

            Response.Flush();
            Response.Close();
            Response = new PangyaBinaryWriter();
        }
        public void SendResponse(List<byte[]> list)
        {
            list.ForEach(item => Response.Write(item));
            SendResponse();
        }

        public void SendResponse(byte[] message)
        {
            Response.Write(message);
            SendResponse();
        }
        /// <summary>
        /// Envia para todos os players conectados
        /// </summary>
        /// <param name="message"></param>
        public void SendToAll(byte[] message)
        {
            Server.SendToAll(message);
        }
        
        public void SendBytes(byte[] send)
        {
            Tcp.GetStream().Write(send, 0, send.Length);
        }
    }
}
