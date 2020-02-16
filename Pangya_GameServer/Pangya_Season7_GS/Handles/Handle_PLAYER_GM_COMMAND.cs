using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_GM_COMMAND : HandleBase<Packet_PLAYER_GM_COMMAND>
    {
        public Handle_PLAYER_GM_COMMAND(Player player) : base(player)
        {
            Handle(Player.CurrentPacket.Reader);
        }

        private void Handle(PangyaBinaryReader reader)
        {
            var response = new PangyaBinaryWriter();

            switch (PacketResult.Command)
            {
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Help:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Status:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Visible:
                    {
                        var visible = reader.ReadByte();
                    }
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Whisper:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Channel:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.List:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Open:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Open2:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Wind:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Weather:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Notice:
                    {
                        var notice = reader.ReadPStr();

                        response.Write(new byte[] { 0x40, 0x00, 0x07 });
                        response.WritePStr(Player.Member_Old.Nickname);
                        response.WritePStr(notice);
                    }
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.GiveItem:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.GoldenBell:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.SetMission:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.MathMap:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.KickPlayer:
                    break;
                case Packet_PLAYER_GM_COMMAND.GMCommandTypeEnum.Discon_Uid:
                    break;
                default:
                    break;
            }
        }
    }
}
