using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository.Models
{
    public class UserStatistics
    {
        public int UID { get; set; }

        public int Drive { get; set; }

        public int Putt { get; set; }

        public int Playtime { get; set; }

        public decimal Longest { get; set; }

        public int Distance { get; set; }

        public int Pangya { get; set; }

        public int Hole { get; set; }

        public int TeamHole { get; set; }

        public int Holeinone { get; set; }

        public int OB { get; set; }

        public int Bunker { get; set; }

        public int Fairway { get; set; }

        public int Albatross { get; set; }

        public int Holein { get; set; }

        public int Pang { get; set; }

        public int Timeout { get; set; }

        public short Game_Level { get; set; }

        public int Game_Point { get; set; }

        public int PuttIn { get; set; }

        public decimal LongestPuttIn { get; set; }

        public decimal LongestChipIn { get; set; }

        public int NoMannerGameCount { get; set; }

        public int ShotTime { get; set; }

        public int GameCount { get; set; }

        public int DisconnectGames { get; set; }

        public int wTeamWin { get; set; }

        public int wTeamGames { get; set; }

        public short LadderPoint { get; set; }

        public short LadderWin { get; set; }

        public short LadderLose { get; set; }

        public short LadderDraw { get; set; }

        public int ComboCount { get; set; }

        public int MaxComboCount { get; set; }

        public int TotalScore { get; set; }

        public short BestScore0 { get; set; }

        public short BestScore1 { get; set; }

        public short BestScore2 { get; set; }

        public short BestScore3 { get; set; }

        public short BESTSCORE4 { get; set; }

        public int? MaxPang0 { get; set; }

        public int? MaxPang1 { get; set; }

        public int? MaxPang2 { get; set; }

        public int? MaxPang3 { get; set; }

        public int? MAXPANG4 { get; set; }

        public int SumPang { get; set; }

        public short LadderHole { get; set; }

        public int GameCountSeason { get; set; }

        public Int64 SkinsPang { get; set; }

        public int SkinsWin { get; set; }

        public int SkinsLose { get; set; }

        public int SkinsRunHoles { get; set; }

        public int SkinsStrikePoint { get; set; }

        public int SkinsAllinCount { get; set; }

        public int EventValue { get; set; }

        public int EventFlag { get; set; }

        public byte[] PL_Statistic()
        {
            var result = new PangyaBinaryWriter();

            result.Write((UInt32)Drive);
            result.Write((UInt32)Putt);
            result.Write((UInt32)Playtime);
            result.Write((UInt32)ShotTime);
            result.Write((Single)Longest);
            result.Write((UInt32)Pangya);
            result.Write((UInt32)Timeout);
            result.Write((UInt32)OB);
            result.Write((UInt32)Distance);
            result.Write((UInt32)Hole);
            result.Write((UInt32)TeamHole);
            result.Write((UInt32)Holeinone);
            result.Write((UInt16)Bunker);
            result.Write((UInt32)Fairway);
            result.Write((UInt32)Albatross);
            result.Write((UInt32)Holein);
            result.Write((UInt32)PuttIn);
            result.Write((Single)LongestPuttIn);
            result.Write((Single)LongestChipIn);
            result.Write((UInt32)Game_Point);
            result.Write((byte)Game_Level);
            result.Write((ulong)Pang);//pangs inicias
            result.Write((UInt32)TotalScore);
            result.Write((byte)BestScore0);
            result.Write((byte)BestScore1);
            result.Write((byte)BestScore2);
            result.Write((byte)BestScore3);
            result.Write((byte)BESTSCORE4);
            result.Write((byte)0x00);//unknow
            result.Write((ulong)MaxPang0);
            result.Write((ulong)MaxPang1);
            result.Write((ulong)MaxPang2);
            result.Write((ulong)MaxPang3);
            result.Write((ulong)MAXPANG4);
            result.Write((ulong)SumPang);
            result.Write((UInt32)GameCount);
            result.Write((UInt32)DisconnectGames);
            result.Write((UInt32)wTeamWin);
            result.Write((UInt32)wTeamGames);
            result.Write((UInt32)LadderPoint);
            result.Write((UInt32)LadderWin);
            result.Write((UInt32)LadderLose);
            result.Write((UInt32)LadderDraw);
            result.Write((UInt32)LadderHole);
            result.Write((UInt32)ComboCount);
            result.Write((UInt32)MaxComboCount);
            result.Write((UInt32)NoMannerGameCount);
            result.Write((ulong)SkinsPang);
            result.Write((UInt32)SkinsWin);
            result.Write((UInt32)SkinsLose);
            result.Write((UInt32)SkinsRunHoles);
            result.Write((UInt32)SkinsStrikePoint);
            result.Write((UInt32)SkinsAllinCount);
            result.Write((byte)0x00); //Unknow2[0]
            result.Write((byte)0x00); //Unknow2[1]
            result.Write((byte)0x00); //Unknow2[2]
            result.Write((byte)0x00); //Unknow2[3]
            result.Write((byte)0x00); //Unknow2[4]
            result.Write((byte)0x00); //Unknow2[5]
            result.Write(GameCountSeason);
            result.Write((byte)0x00); //Unknow3[0]
            result.Write((byte)0x00); //Unknow3[1]
            result.Write((byte)0x00); //Unknow3[2]
            result.Write((byte)0x00); //Unknow3[3]
            result.Write((byte)0x00); //Unknow3[4]
            result.Write((byte)0x00); //Unknow3[5]
            result.Write((byte)0x00); //Unknow3[6]
            result.Write((byte)0x08); //Unknow3[7]

            return result.GetBytes();
        }


    }
}
