using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public partial class Player
    {
        public class PlayerMemberInfo
        {
            public int UID { get; set; }

            public ushort GameID { get; set; } = 65355;
            public byte? IDState { get; set; }

            public byte? Logon { get; set; }

            public byte? FirstSet { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

            public string Nickname { get; set; }

            public string AuthKey_Login { get; set; }
            public string AuthKey_Game { get; set; }
            public int? Cookie { get; set; }
            public int? GUILDINDEX { get; set; }
            public string LockerPWD { get; set; }

            public int? LockerPang { get; set; }

            public byte? Sex { get; set; }
            public byte? Capabilities { get; set; }

            public int? DailyLoginCount { get; set; }
            public DateTime? LastLogonTime { get; set; }
            public uint CHARACTER_ID { get; set; }

            public uint Visible { get; set; } = 0;

            public int Title_TypeID { get; set; } = 0;
        }

        public class PlayerMemberMacro
        {
            public string Macro1 { get; set; }
            public string Macro2 { get; set; }
            public string Macro3 { get; set; }
            public string Macro4 { get; set; }
            public string Macro5 { get; set; }
            public string Macro6 { get; set; }
            public string Macro7 { get; set; }
            public string Macro8 { get; set; }
            public string Macro9 { get; set; }
            public string Macro10 { get; set; }
        }

        public class PlayerMemberGuild
        {
            public int GUILD_TOTAL_MEMBER { get; set; }
            public int GUILD_INDEX { get; set; }
            public int GUILD_LEADER_UID { get; set; }
            public int? GUILD_POINT { get; set; }
            public int? GUILD_PANG { get; set; }
            public int? GUILD_IMAGE_KEY_UPLOAD { get; set; }
            public string GUILD_NAME { get; set; }
            public string GUILD_IMAGE { get; set; }
            public string GUILD_NOTICE { get; set; }
            public string GUILD_INTRODUCING { get; set; }
            public string GUILD_LEADER_NICKNAME { get; set; }
            public byte? GUILD_POSITION { get; set; }
            public byte? GUILD_VALID { get; set; }
            public DateTime? GUILD_CREATE_DATE { get; set; }

        }

        public class PlayerMemberRecordInfo
        {
            public int ID { get; set; }
            public short Map { get; set; }
            public int Drive { get; set; }
            public int Putt { get; set; }
            public int Hole { get; set; }
            public int Fairway { get; set; }
            public int Holein { get; set; }
            public int PuttIn { get; set; }
            public int TotalScore { get; set; }
            public short BestScore { get; set; }
            public int MaxPang { get; set; }
            public int CharTypeId { get; set; }
            public byte EventScore { get; set; }
            public byte Assist { get; set; }
        }
    }
}
