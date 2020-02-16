using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository.Models
{
    public class Caddie
    {
        /// <summary>
        /// Caddie ID
        /// </summary>
        public int CID { get; set; }

        /// <summary>
        /// Usuário ID
        /// </summary>
        public int UID { get; set; }

        public int TYPEID { get; set; }

        public int EXP { get; set; }

        public byte cLevel { get; set; }

        public int? SKIN_TYPEID { get; set; }

        public byte? RentFlag { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? END_DATE { get; set; }

        public DateTime? SKIN_END_DATE { get; set; }

        public byte? TriggerPay { get; set; }

        public byte VALID { get; set; }

        //Calculado pela Proc
        public int SKIN_HOUR_LEFT { get; set; }

        //Calculado pela Proc
        public int DAY_LEFT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Total 25 bytes</returns>
        public byte[] GetData()
        {
            var result = new PangyaBinaryWriter();
            result.Write(CID);
            result.Write(TYPEID);
            result.Write(SKIN_TYPEID.Value);
            result.Write(cLevel);
            result.Write(EXP);
            result.Write(RentFlag.Value);
            result.Write((ushort)DAY_LEFT);
            result.Write((ushort)SKIN_HOUR_LEFT);
            result.Write((byte)0x00);
            result.Write((ushort)TriggerPay);

            return result.GetBytes();

        }
    }
}
