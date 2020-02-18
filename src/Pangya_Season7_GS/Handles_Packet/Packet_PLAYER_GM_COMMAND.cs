using PangyaAPI.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_GM_COMMAND : PacketResult
    {
        public enum GMCommandTypeEnum
        {
            Help = 1,
            Status = 2,
            Visible = 3, //OK
            Whisper = 4,
            Channel = 5,
            List = 6,

            Open = 8,
            Open2 = 9,
            Wind = 14,
            Weather = 15,
            Notice = 17,
            GiveItem = 18,
            GoldenBell = 19,
            SetMission = 28,
            MathMap = 31,
            KickPlayer = 10,
            Discon_Uid = 11,
        }

        //Lista completa de comandos
        /* help
         * command
         * status
         * visible
         * whisper
         * channel
         * list
         * open
         * close
         * kick
         * disconnect
         * discon_uid
         * destroy
         * wind
         * weather
         * identity
         * notice
         * giveitem
         * goldenbell
         * setprize
         * unsetprize
         * showprize
         * loadscript
         * noticeprize
         */

        public GMCommandTypeEnum Command { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Command = (GMCommandTypeEnum)reader.ReadUInt16();
        }
    }
}
