using PangyaAPI;
using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_SELECT_LOBBY : PacketResult
    {
        public byte ChannelId { get; set; }

        public enum ChannelResult
        {
           AllowAcess = 0x01, //Libera acesso ao canal
           IsFull = 0x02, //Está cheio
           CannotFind = 0x03, //Não encontrado
           Failed = 0x04 //Erro ao acessar
        }

        public override void Load(PangyaBinaryReader reader)
        {
            this.ChannelId = reader.ReadByte();
        }

        public byte[] GetChannelResult(ChannelResult result)
        {
            return new byte[] { 0x4E, 0x00, (byte)result };
        }

    }
}
