using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;
using System;
using System.IO;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_START_GAME : HandleBase<Packet_PLAYER_START_GAME>
    {
        public Handle_PLAYER_START_GAME(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var game = Player.Game;
            game.GameStarted = true;

            Player.Channel.Lobby.GameManager.Update(game);

            game.Players.ForEach(p =>
            {
                ServerRatePang(10, p);

                if (game.Mode == GameTypeEnum.TOURNEY)
                {
                    p.SendResponse(new byte[] { 0x76, 0x00, 0x04, 0x01, 0x00, 0x00, 0x00, 0xE3, 0x07, 0x07, 0x00, 0x02, 0x00, 0x09, 0x00, 0x11, 0x00, 0x1C, 0x00, 0x02, 0x00, 0x1E, 0x00 });

                }
                else if (game.Mode == GameTypeEnum.VERSUS_STROKE)
                {
                    //Player.SendResponse(File.ReadAllBytes(@"C:\Users\Administrador\Desktop\VS\PLAYER_CHANGE_EQUIPMENT\1 - 76 00.ug"));
                }

                //HandleBuildHole(p);
            });

        }

        private void ServerRatePang(int serverPangRate, Player player)
        {
            player.SendResponse(new byte[] { 0x30, 0x02 });
            player.SendResponse(new byte[] { 0x31, 0x02 });

            var response = new PangyaBinaryWriter();
            response.Write(new byte[] { 0x77, 0x00 });
            response.Write(serverPangRate);

            player.SendResponse(response.GetBytes());

        }

        private void HandleBuildHole(Player player)
        {
            var game = Player.Game;

            var response = new PangyaBinaryWriter();

            response.Write(new byte[] { 0x52, 0x00 });
            response.Write((byte)game.Course); //mapa
            response.Write((byte)game.Mode); //type game?
            response.Write((byte)game.HoleOrder);//mode game
            response.Write(game.Holes); //hole total
            response.Write(game.Trophy); //id do trofeu
            response.Write(game.TimeSec); //vs?//Game.GameInfo.TurnTime
            response.Write(game.TimeMin);
            response.Write(GetHoleBuild(game));

            response.Write(new byte[] {
                0x03, 0x4F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00 });
            //response.WriteEmptyBytes(22);

            player.SendResponse(response.GetBytes());
        }

        private byte[] GetHoleBuild(Game game)
        {
            var result = new PangyaBinaryWriter();
            foreach (var H in game.HolesInfo)
            {
                result.Write(new Random().Next());
                result.Write(H.Pos);
                result.Write(H.Course);
                result.Write(H.Hole);
            }
            var GetBytes = result.GetBytes();
            return GetBytes;
        }
    }
}
