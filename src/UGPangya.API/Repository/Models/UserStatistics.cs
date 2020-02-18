using UGPangya.API.BinaryModels;

namespace UGPangya.API.Repository.Models
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

        public long SkinsPang { get; set; }

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

            result.Write((uint) Drive);
            result.Write((uint) Putt);
            result.Write((uint) Playtime);
            result.Write((uint) ShotTime);
            result.Write((float) Longest);
            result.Write((uint) Pangya);
            result.Write((uint) Timeout);
            result.Write((uint) OB);
            result.Write((uint) Distance);
            result.Write((uint) Hole);
            result.Write((uint) TeamHole);
            result.Write((uint) Holeinone);
            result.Write((ushort) Bunker);
            result.Write((uint) Fairway);
            result.Write((uint) Albatross);
            result.Write((uint) Holein);
            result.Write((uint) PuttIn);
            result.Write((float) LongestPuttIn);
            result.Write((float) LongestChipIn);
            result.Write((uint) Game_Point);
            result.Write((byte) Game_Level);
            result.Write((ulong) Pang); //pangs inicias
            result.Write((uint) TotalScore);
            result.Write((byte) BestScore0);
            result.Write((byte) BestScore1);
            result.Write((byte) BestScore2);
            result.Write((byte) BestScore3);
            result.Write((byte) BESTSCORE4);
            result.Write((byte) 0x00); //unknow
            result.Write((ulong) MaxPang0);
            result.Write((ulong) MaxPang1);
            result.Write((ulong) MaxPang2);
            result.Write((ulong) MaxPang3);
            result.Write((ulong) MAXPANG4);
            result.Write((ulong) SumPang);
            result.Write((uint) GameCount);
            result.Write((uint) DisconnectGames);
            result.Write((uint) wTeamWin);
            result.Write((uint) wTeamGames);
            result.Write((uint) LadderPoint);
            result.Write((uint) LadderWin);
            result.Write((uint) LadderLose);
            result.Write((uint) LadderDraw);
            result.Write((uint) LadderHole);
            result.Write((uint) ComboCount);
            result.Write((uint) MaxComboCount);
            result.Write((uint) NoMannerGameCount);
            result.Write((ulong) SkinsPang);
            result.Write((uint) SkinsWin);
            result.Write((uint) SkinsLose);
            result.Write((uint) SkinsRunHoles);
            result.Write((uint) SkinsStrikePoint);
            result.Write((uint) SkinsAllinCount);
            result.Write((byte) 0x00); //Unknow2[0]
            result.Write((byte) 0x00); //Unknow2[1]
            result.Write((byte) 0x00); //Unknow2[2]
            result.Write((byte) 0x00); //Unknow2[3]
            result.Write((byte) 0x00); //Unknow2[4]
            result.Write((byte) 0x00); //Unknow2[5]
            result.Write(GameCountSeason);
            result.Write((byte) 0x00); //Unknow3[0]
            result.Write((byte) 0x00); //Unknow3[1]
            result.Write((byte) 0x00); //Unknow3[2]
            result.Write((byte) 0x00); //Unknow3[3]
            result.Write((byte) 0x00); //Unknow3[4]
            result.Write((byte) 0x00); //Unknow3[5]
            result.Write((byte) 0x00); //Unknow3[6]
            result.Write((byte) 0x08); //Unknow3[7]

            return result.GetBytes();
        }
    }
}