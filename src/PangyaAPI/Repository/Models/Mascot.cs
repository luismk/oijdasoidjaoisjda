using PangyaAPI.BinaryModels;
using PangyaAPI.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository.Models
{
    public class Mascot
    {
        public int MID { get; set; }
        public int MASCOT_TYPEID { get; set; }
        public string MESSAGE { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? END_DATE_INT { get; set; }

        public byte[] GetMascotInfo()
        {
            var result = new PangyaBinaryWriter();

            result.Write(MID);
            result.Write(MASCOT_TYPEID);
            result.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 });
            result.WriteStr(MESSAGE, 16);
            result.WriteEmptyBytes(length: 14);
            result.Write((short)END_DATE_INT.Value);
            result.Write(DateEnd?.ToPangyaDateTime());
            result.Write((byte)0);

            return result.GetBytes();
        }
    }
}
