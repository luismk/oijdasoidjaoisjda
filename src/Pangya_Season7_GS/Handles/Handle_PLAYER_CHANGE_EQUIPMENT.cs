using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;
using System;
using System.IO;
using System.Linq;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_CHANGE_EQUIPMENT : HandleBase<Packet_PLAYER_CHANGE_EQUIPMENT>
    {
        public Handle_PLAYER_CHANGE_EQUIPMENT(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            switch (PacketResult.Action)
            {
                case ChangeEquipmentEnumB.GetCaddieSelected:
                    {
                        //response.Write(Player.Caddies.First().GetData());
                    }
                    break;
                case ChangeEquipmentEnumB.GetClubSelected:
                    {
                        //response.Write(Player.WarehouseCollection.GetClubData());
                    }
                    break;
                case ChangeEquipmentEnumB.GetBallSelected:
                    {
                        //response.Write(PacketResult.Id);
                    }
                    break;
                case ChangeEquipmentEnumB.GetCharSelected:
                    {
                        if (Player.Game == null)
                            return;


                        foreach (var player in Player.Game.Players)
                        {
                            var response = new PangyaBinaryWriter();

                            response.Write(new byte[] { 0x4B, 0x00 });
                            response.Write(0);
                            response.Write((byte)PacketResult.Action);
                            response.Write(Player.ConnectionId);
                            response.Write(Player.Characters.GetCharData());

                            player.SendResponse(response.GetBytes());
                        }
                    }
                    break;
                case ChangeEquipmentEnumB.GetMascotSelected:
                    {
                        //response.Write(Player.Mascots.First()?.GetMascotInfo() ?? new byte[0x3E]);//62
                    }
                    break;
                case ChangeEquipmentEnumB.GameStart:
                    {
                        HandleAcquireData();
                    }
                    break;
            }

        }

        private void HandleAcquireData()
        {
            //76 00
            #region 76 00
            var packet = new PangyaBinaryWriter();

            packet.Write(new byte[] { 0x76, 0x00, 0x00 });

            packet.Write(GetVSInformation());

            Player.Game.Players.ForEach(p => p.SendResponse(packet.GetBytes()));

            #endregion

            //45 00
            #region 45 00
            var packet2 = new PangyaBinaryWriter();
            packet2.Write(new byte[] { 0x45, 0x00, });

            packet2.Write(Player.User_Statistics.PL_Statistic());
            packet2.Write(new byte[]
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            });

            Player.SendResponse(packet2.GetBytes());

            #endregion

            //52 00
            #region 52 00
            HandleBuildHole();
            #endregion

            //6A 01
            #region 6A 01
            Player.SendResponse(new byte[] { 0x6A, 0x01, 0x3F, 0x6B, 0x00, 0x00 });
            #endregion

            //48 00
            #region 48 00
            Player.Game.CreateVSStroke();
            #endregion

            return;





            //48 00
        }

        private byte[] GetVSInformation()
        {
            var result = new PangyaBinaryWriter();

            result.Write((byte)Player.Game.Players.Count);

            Player.Game.Players.ForEach(p => result.Write(p.GetGameInfoVS()));

            return result.GetBytes();
        }


        public void HandleBuildHole()
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
            response.WriteEmptyBytes(22);

            Player.SendResponse(response.GetBytes());
        }

        public byte[] GetHoleBuild(Game game)
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
