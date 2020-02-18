using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository.Models
{
    public class Warehouse
    {
        public int IDX { get; set; }
        public int TYPEID { get; set; }
        public short? C0 { get; set; }
        public short? C1 { get; set; }
        public short? C2 { get; set; }
        public short? C3 { get; set; }
        public short? C4 { get; set; }
        public byte? Flag { get; set; }
        public int? HOURLEFT { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? DateEnd { get; set; }
        public string UCC_UNIQE { get; set; }
        public byte? UCC_STATUS { get; set; }
        public int? UCC_COPY_COUNT { get; set; }
        public string UCC_NAME { get; set; }
        public string UCC_DRAWER { get; set; }
        public int? UCC_DRAWER_UID { get; set; }
        public int? CLUB_POINT { get; set; }
        public int? CLUB_WORK_COUNT { get; set; }
        public short? C0_SLOT { get; set; }
        public short? C1_SLOT { get; set; }
        public short? C2_SLOT { get; set; }
        public short? C3_SLOT { get; set; }
        public short? C4_SLOT { get; set; }
        public int? CLUB_SLOT_CANCEL { get; set; }
        public int? CLUB_POINT_TOTAL_LOG { get; set; }
        public int? CLUB_UPGRADE_PANG_LOG { get; set; }
    }
}
