using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;
using System.Linq;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_SHOT_SYNC : HandleBase<Packet_PLAYER_SHOT_SYNC>
    {
        public Handle_PLAYER_SHOT_SYNC(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            HandleGameDropItem(Player.Game);

            Player.Game.SendToAll(new byte[] { 0x5B, 0x00,
                0x01, //Alternável
                0x00,
                0xD3, 0x00, //Alternável
                0x01 });

            //        res.WriteStr(#$5B#$00);
            //res.WriteUInt16(Wind.windpower);
            //        res.WriteUInt8(byte(random(255)));
            //        res.WriteStr(#$00#$01);

            HandleNextPlayer(Player.Game);

        }

        public void HandleGameDropItem(Game game)
        {
            foreach (var player in game.Players)
            {
                var response = new PangyaBinaryWriter();

                response.Write(new byte[] { 0xCC, 0x00 });
                response.Write(player.ConnectionId);
                response.WriteByte(0);

                Player.Game.SendToAll(response);
                //Player.SendResponse(response);
            }
        }

        public void HandleNextPlayer(Game game)
        {
            //Obtém connectionId do próximoPlayer
            var connectionId = game.Players.First(p => p.ConnectionId != Player.ConnectionId).ConnectionId;

            var result = new PangyaBinaryWriter();
            result.Write(new byte[] { 0x63, 0x00 });
            result.Write(connectionId);

            game.SendToAll(result);
            //Player.SendResponse(new byte[] { 0x63, 0x00, 0x01, 0x00, 0x00, 0x00 });
        }
    }
}
