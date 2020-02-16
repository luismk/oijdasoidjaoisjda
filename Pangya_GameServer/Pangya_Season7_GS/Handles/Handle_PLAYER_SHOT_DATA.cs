using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using System;
using System.Device.Location;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_SHOT_DATA : HandleBase<Packet_PLAYER_SHOT_DATA>
    {
        public Handle_PLAYER_SHOT_DATA(Player player) : base(player)
        {
            base.PacketResult.Load(player.CurrentPacket.Reader, player.Game.RoomKey);

            Handle();
        }

        private void Handle()
        {
            Console.WriteLine($"Pangs: {PacketResult.Pang} | BonusPang: {PacketResult.BonusPang}");

            Console.WriteLine("Hole Pos X: " + Player.HolePos.X);
            Console.WriteLine("Hole Pos Z: " + Player.HolePos.Z);
            Console.WriteLine("Shot POS X: " + PacketResult.X);
            Console.WriteLine("Shot POS Z: " + PacketResult.Z);



            //CALCULO DE DISTÂNCIA

            //HSREINA: 
            // client.Data.GameInfo.holedistance := abs(sqrt(sqr(m_currentHolePos.x - shotData.pos.x) + sqr(m_currentHolePos.z - shotData.pos.z)));


            //ERIC (TESTAR)
            Console.WriteLine("Testando calculo de distância......");
            var holeDistance = Math.Abs((Math.Sqrt(Math.Pow(Player.HolePos.X - PacketResult.X, 2) + Math.Pow(Player.HolePos.Z - PacketResult.Z, 2))));
            Console.WriteLine("Distância: " + holeDistance);


            var result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0x64, 0x00 });
            result.Write(PacketResult.ResponseShot);
            Player.Game.SendToAll(result);
        }

    }
}
