using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_MATCH_DATA : HandleBase<Packet_PLAYER_MATCH_DATA>
    {
        public Handle_PLAYER_MATCH_DATA(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var result = new PangyaBinaryWriter();

      //      WriteStr(#$F7#$01);
      //WriteUInt32(PL.ConnectionID);
      //      WriteUInt8(PL.GameInfo.HolePos);
      //      WriteStr(CP.GetRemainingData);
      //      Self.Send(Packet);

            result.Write(new byte[] { 0xF7, 0x01 });
            result.Write(Player.ConnectionId);
            result.Write(new byte[] { 0x01 });
            result.Write(PacketResult.ShotData);

            Player.Game.Players.ForEach(p => p.SendResponse(result.GetBytes()));

            //var packt = Player.CurrentPacket.Reader.GetBytes();

            //for (int i = 0; i < packt.Length; i++)
            //{
            //    var decrypted = DecryptShot(packt.Skip(i).ToArray(), Player.Game.RoomKey);
            //    File.WriteAllBytes($@"C:\Users\Administrador\Desktop\ShotDataDecrypted\ShotDecrypted_{i}.ug", decrypted);
            //}


            result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0x6E, 0x00 });
            result.Write(Player.ConnectionId);
            result.Write(new byte[] { 0x01, 0x92, 0x0B, 0x4B, 0xC3, 0xA3, 0x6E, 0xB8, 0x41, 0x00, 0x08, 0x10, 0x00, 0x10, 0x02 });

            Player.Game.Players.ForEach(p => p.SendResponse(result.GetBytes()));
        }

        private byte[] DecryptShot(byte[] data, byte[] gameKey)
        {
            var decrypted = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                decrypted[i] = (byte)(data[i] ^ gameKey[i % 16]);
            }

            return decrypted;
        }


    }
}
