using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository.Models
{
    public class Guild
    {
        public int GUILD_INDEX { get; set; }
        public string GUILD_NAME { get; set; }
        public string GUILD_INTRODUCING { get; set; }
        public string GUILD_NOTICE { get; set; }
        public int GUILD_PANG { get; set; }
        public int GUILD_POINT { get; set; }
        public int GUILD_LEADER_UID { get; set; }
        public DateTime? GUILD_CREATE_DATE { get; set; }
        public int GUILD_TOTAL_MEMBER { get; set; }
        public string GUILD_LEADER_NICKNAME { get; set; }
        public byte GUILD_POSITION { get; set; }
        public string GUILD_IMAGE { get; set; }
    }
}
