using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UGPangya.API.Models
{
    public class GameSlot
    {
        public int SlotNumber { get; set; }

        public bool IsMaster { get; set; }

        public bool ReadyToPlay { get; set; }
    }
}
