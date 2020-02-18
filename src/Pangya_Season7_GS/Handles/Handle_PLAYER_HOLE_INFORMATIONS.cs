using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_HOLE_INFORMATIONS : HandleBase<Packet_PLAYER_HOLE_INFORMATIONS>
    {
        public Handle_PLAYER_HOLE_INFORMATIONS(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            Player.HolePos = new HolePos()
            {
                X = PacketResult.X,
                Z = PacketResult.Z
            };

            HandleGameWeather();
            GameWind(7, 125);
        }

        private void GameWind(ushort windPower, ushort windDirection)
        {
            var response = new PangyaBinaryWriter();

            response.Write(new byte[] { 0x5B, 0x00 });
            response.Write(windPower);
            response.Write(windDirection);
            response.Write((byte)1);

            Player.Game.Players.ForEach(p => p.SendResponse(response.GetBytes()));
        }

        private void HandleGameWeather()
        {
            var response = new PangyaBinaryWriter();

            response.Write(new byte[] { 0x9E, 0x00 });
            response.Write(new byte[] { 0x00, 0x00, 0x00 });
            //response.Write(0); //type
            //response.Write((byte)0);

            Player.Game.Players.ForEach(p => p.SendResponse(response.GetBytes()));
        }
    }
}
