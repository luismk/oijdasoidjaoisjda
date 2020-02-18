using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaConnector.Repository.Model
{
    public class MemberModel
    {
        public int UID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public byte IDState { get; set; }

        public byte FirstSet { get; set; }

        public DateTime? LastLogonTime { get; set; }

        public byte Logon { get; set; }

        public string Nickname { get; set; }

        public byte Sex { get; set; }

        public string IPAddress { get; set; }

        public int LogonCount { get; set; }

        public byte Capabilities { get; set; }

        public DateTime RegDate { get; set; }

        public string AuthKey_Login { get; set; }

        public string AuthKey_Game { get; set; }

        public int GUILDINDEX { get; set; }

        public int DailyLoginCount { get; set; }
    }
}
