using Pangya_MessengerServer.DataBase;
using PangyaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_MessengerServer
{
    public class MPlayer : Player
    {
        private static PangyaEntities _db;

        public MPlayer(TcpClient tcp) : base(tcp)
        {
            _db = new PangyaEntities();
        }

        public void HandleClientReciveUID()
        {
            var uid = CurrentPacket.Reader.ReadInt32();

            var userId = CurrentPacket.Reader.ReadPStr(); //taiwan

            var query = $"SELECT* FROM Pangya_Member WHERE UID = '{uid}'";

            var statiscs = $"SELECT* FROM Pangya_User_Statistics WHERE UID = '{uid}'";

            UserInfo = _db.Database.SqlQuery<PlayerMemberInfo>(query).FirstOrDefault();

            InfoStatistic = _db.Database.SqlQuery<PlayerStaticInfo>(statiscs).FirstOrDefault();


            Response.Write(new byte[] { 0x2F, 0x00, 0x00 });
            Response.Write(uid); //uid player
            SendResponse();
        }

        /// <summary>
        /// parece que envia algum tipo de informação entre GS+canal, e o MSG
        /// </summary>
        /// <param name="player"></param>
        public void Test23()
        {
            var unk1 = CurrentPacket.Reader.ReadUInt16();//canal id?
            var unk2 = CurrentPacket.Reader.ReadUInt32(); //time?
             CurrentPacket.Reader.Skip(1);
            var serverID = CurrentPacket.Reader.ReadUInt32(); //char index??
            var channelID = CurrentPacket.Reader.ReadByte();
            var ChannelName = CurrentPacket.Reader.ReadPStr();

            Response.Write(new byte[] { 0x30, 0x00, 0x15, 0x01 });
            Response.Write(UserInfo.UID); //uid player
            Response.Write((uint)UserInfo.Capabilities); //será a capacidade?
            Response.Write(channelID); //id do canal 
            Response.Write((ushort)serverID); //game id talvez
            Response.Write((uint)15);//desconhecido
            Response.Write((UInt32)serverID); // porta que player se conectou
            Response.Write(new byte[] { 0xff }); //??
            Response.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, });
            SendResponse();
        }

        public void Test23_1()
        {
          Response.Write(new byte[] 
          {
              0x30, 0x00, 0x02, 0x01, 0x01,
              0x00, 0x00, 0x00, 0x00 
          });
           SendResponse();
        }
        public void Test23_2()
        {
            Response.Write(new byte[] { 0x30, 0x00, 0x15, 0x01 });
            Response.Write((UInt32)UserInfo.UID); //uid player
            Response.Write((UInt32)67108864);
            Response.Write((byte)0x01);
            Response.Write((byte)0xff); //game id talvez
            Response.Write(new byte[]
            {
                0x80, 0x00, //sexo talvez
                0x00, 0x03
            });
            Response.Write((UInt32)20201); // porta que player se conectou
            Response.WriteStr("Canal livre#1", 20);
            SendResponse();
        }

        internal void RequestUnknown14()
        {
            Response.Write(new byte[]{ 0x30, 0x00, 0x15 });
            Response.Write((UInt32)UserInfo.UID); //uid player
            Response.Write((UInt32)4);
            Response.Write((byte)0x01);
            Response.Write((UInt16)65535);
            Response.Write((UInt32)96);
            Response.Write((UInt32)20201);//porta do gs, que o player se conectou
            Response.Write(new byte[]
            {
                0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            });

            SendResponse();

            Send(new byte[]
                       {
                            0x30, 0x00, 0x02, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00
                       });

        }

        public void PlayerSearchFriend()
        {

            var friend = "eantonio";

            var nick = friend.ToString();

            var search = _db.Pangya_Member.Any(p => p.Nickname == friend);
            if (search == true)
            {
                Console.WriteLine("ok");
                //encontrado
                Response.Write(new byte[]
                {
                    0x30, 0x00, 0x17,
                    0x01, 0x00, 0x00,
                    0x00, 0x00
                });
                Response.WritePStr(friend); //nickname 
                Response.Write((UInt32)4276); //uid do player?
                SendResponse();
            }
            else if (search == false)
            {
                //não encontrado
                SendResponse(new byte[]
                {
                0x30, 0x00, 0x17, 0x01, 0x01, 0x00, 0x00, 0x00
                });
            }
        }

        internal void RequestFriend()
        {
            Response.Write(new byte[]
            {
                0x30, 0x00, 0x04,
                0x01, 0x00, 0x00,
                0x00, 0x00
            });
            Response.WriteStr(UserInfo.Nickname, 22);
            Response.WriteStr("teste", 11); //nickname do amigo
            Response.Write(UserInfo.UID); //meu uid
            Response.Write(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            Response.Write((UInt32)0);
            Response.Write(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            Response.WriteEmptyBytes(12);
            Response.Write(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });
            Response.Write((UInt32)0);
            Response.Write(new byte[] { 0xFF });
            Response.WriteEmptyBytes(64);
            Response.Write(new byte[] { 0x05, 0xFF, 0x00, 0x08, 0x01 });

            SendResponse();

            Response.Write(new byte[] { 0x30, 0x00 });
            Response.WritePStr(UserInfo.Nickname); //??
            Response.WritePStr("teste");

            SendResponse();
        }
    }
}
