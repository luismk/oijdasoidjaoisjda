using PangyaCryptography;
using PangyaAPI.BinaryModels;
//using PangyaCryptography;
using System;
using System.IO;
using System.Text;

namespace PangyaAPI
{
    public class Packet
    {
        #region Private Fields

        private MemoryStream _stream;

        #endregion

        #region Public Fields

        /// <summary>
        /// Leitor do packet
        /// </summary>
        public PangyaBinaryReader Reader;

        /// <summary>
        /// Id do Packet
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        /// Mensagem do Packet
        /// </summary>
        public byte[] Message { get; set; }

        public byte[] MessageCrypted { get; set; }

        #endregion

        #region Constructor

        public Packet(byte[] message, byte key)
        {
            Id = BitConverter.ToInt16(new byte[] { message[5], message[6] }, 0);

            MessageCrypted = new byte[message.Length];
            Buffer.BlockCopy(message, 0, MessageCrypted, 0, message.Length); //Copia mensagem recebida criptografada

            Message = ClientCipher.Decrypt(message, key);

            _stream = new MemoryStream(Message);

            _stream.Seek(2, SeekOrigin.Current); //Seek Inicial
            Reader = new PangyaBinaryReader(_stream);
        }

        #endregion
    }
}
