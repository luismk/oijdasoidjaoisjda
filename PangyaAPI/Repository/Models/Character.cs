using PangyaAPI.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository.Models
{
    public class Character
    {   
        public int CID { get; set; }

        public int UID { get; set; }

        public int TYPEID { get; set; }

        public ushort GIFT_FLAG { get; set; }

        public ushort HAIR_COLOR { get; set; }

        public byte? POWER { get; set; }

        public byte? CONTROL { get; set; }

        public byte? IMPACT { get; set; }

        public byte? SPIN { get; set; }

        public byte? CURVE { get; set; }

        public int CUTIN { get; set; }

        public CharacterEquip CharacterEquip { get; set; }

        public CardEquipCollection CardEquipCollection { get; set; }
    }
}
