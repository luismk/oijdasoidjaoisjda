using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Models
{
    public class ChatGameInfo
    {

        public PlayerPostureEnum Posture { get; set; }

        public PlayerActionEnum LastAction { get; set; }

        public byte[] Animation { get; set; }

        public int Animation_2 { get; set; }

        public string AnimationWithEffects { get; set; }
    }
}
