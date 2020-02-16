using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public class HolePos
    {
        public float X { get; set; }

        public float Z { get; set; }

        public float GetDistance()
        {
            return (X + Z);
        }
    }
}
