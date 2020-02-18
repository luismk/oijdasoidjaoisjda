using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Collections
{
    public class ChannelCollection : List<Channel>
    {
        private TcpServer _server { get; set; }

        public ChannelCollection(TcpServer server)
        {
            _server = server;
        }

        public byte[] GetData()
        {
            var result = new PangyaBinaryWriter(new MemoryStream());
            result.Write(new byte[] { 0x4D, 0x00, Convert.ToByte(Count) });
            this.ForEach(channel =>
            {
                result.WriteStr(channel.Name, 64);
                result.Write(BitConverter.GetBytes(channel.MaxPlayers));
                result.Write(BitConverter.GetBytes((ushort)channel.Players.Model.Count)); //(ushort)channel.Players.ToList().Count()
                result.Write(channel.Id); //Lobby ID
                result.Write((int)channel.Flag);//tipo de channel                
                result.WriteUInt32(1);//unknow
            });
            var resultBytes = result.GetBytes();
            return resultBytes;
        }

    }
}
