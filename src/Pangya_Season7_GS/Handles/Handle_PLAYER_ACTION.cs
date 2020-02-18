using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Models;
using System;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_ACTION : HandleBase<Packet_PLAYER_ACTION>
    {
        public Handle_PLAYER_ACTION(Player player) : base(player)
        {
            Handle(Player.CurrentPacket.Reader);
        }

        private void Handle(PangyaBinaryReader reader)
        {
            var response = new PangyaBinaryWriter();
            response.Write(new byte[] { 0xC4, 0x00 });
            response.Write(Player.ConnectionId);
            response.Write((byte)PacketResult.Action);

            switch (PacketResult.Action)
            {
                case PlayerActionEnum.PLAYER_ACTION_ROTATION:
                    {
                        Player.Position.W = reader.ReadSingle();

                        response.WriteSingle(Player.Position.W);
                    }
                    break;
                case PlayerActionEnum.PLAYER_ACTION_UNK:
                    break;
                case PlayerActionEnum.PLAYER_ACTION_APPEAR:
                    {
                        Player.Position.X = reader.ReadSingle();
                        Player.Position.Y = reader.ReadSingle();
                        Player.Position.Z = reader.ReadSingle();

                        response.Write(Player.Position.X);
                        response.Write(Player.Position.Y);
                        response.Write(Player.Position.Z);
                    }
                    break;
                case PlayerActionEnum.PLAYER_ACTION_SUB:
                    {
                        Player.ChatGameInfo.Posture = (PlayerPostureEnum)reader.ReadByte();
                        response.Write((int)Player.ChatGameInfo.Posture);
                    }
                    break;
                case PlayerActionEnum.PLAYER_ACTION_MOVE:
                    {
                        var x = reader.ReadSingle();
                        var y = reader.ReadSingle();
                        var z = reader.ReadSingle();

                        Player.Position.X += x;
                        Player.Position.Y += y;
                        Player.Position.Z += z;

                        response.WriteSingle(x);
                        response.WriteSingle(y);
                        response.WriteSingle(z);
                    }
                    break;
                case PlayerActionEnum.PLAYER_ACTION_ANIMATION:
                    {
                        Player.ChatGameInfo.Animation = reader.ReadPStrBytes();
                        response.WritePStrBytes(Player.ChatGameInfo.Animation);
                    }
                    break;
                case PlayerActionEnum.PLAYER_ACTION_ANIMATION_2:
                    {
                        Player.ChatGameInfo.Animation_2 = reader.ReadInt32();

                        response.Write(Player.ChatGameInfo.Animation_2);
                    }
                    break;
                case PlayerActionEnum.PLAYER_ANIMATION_WITH_EFFECTS:
                    {
                        Player.ChatGameInfo.AnimationWithEffects = reader.ReadPStr();
                        response.WritePStr(Player.ChatGameInfo.AnimationWithEffects);
                    }
                    break;
            }

            string notice = $"POS X: {Player.Position.X} Y: {Player.Position.Y} Z: {Player.Position.Z}";
            Player.SendNotice(notice);

            //Console.WriteLine(Player.Member.Nickname + );
            Player.Game.SendToAll(response);

           
        }
    }
}
