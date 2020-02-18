using System;
using System.Collections.Generic;
using System.IO;
using UGPangya.API.BinaryModels;
using UGPangya.API.Repository.Models;

namespace UGPangya.API.Collections
{
    public class ChannelCollection : List<Channel>
    {
        public ChannelCollection(TcpServer server)
        {
            _server = server;
        }

        private TcpServer _server { get; }

        public byte[] GetData()
        {
            var result = new PangyaBinaryWriter(new MemoryStream());
            result.Write(new byte[] {0x4D, 0x00, Convert.ToByte(Count)});
            ForEach(channel =>
            {
                result.WriteStr(channel.Name, 64);
                result.Write(BitConverter.GetBytes(channel.MaxPlayers));
                result.Write(
                    BitConverter.GetBytes((ushort) channel.Players.Model
                        .Count)); //(ushort)channel.Players.ToList().Count()
                result.Write(channel.Id); //Lobby ID
                result.Write((int) channel.Flag); //tipo de channel                
                result.WriteUInt32(1); //unknow
            });
            var resultBytes = result.GetBytes();
            return resultBytes;
        }
    }
}