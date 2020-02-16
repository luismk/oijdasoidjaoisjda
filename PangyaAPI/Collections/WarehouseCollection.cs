using PangyaAPI.BinaryModels;
using PangyaAPI.Repository;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PangyaAPI.Collections
{
    public class WarehouseCollection : List<Warehouse>
    {
        private Player _player { get; set; }
        private WarehouseRepository _warehouseRepository { get; set; }

        public WarehouseCollection(Player player)
        {
            _player = player;
            _warehouseRepository = new WarehouseRepository();
            this.AddRange(_warehouseRepository.GetByUID(player.Member_Old.UID));
        }

        public byte[] GetWarehouseData()
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0x73, 0x00 });
            result.Write((UInt16)Count);//desnecessario conta +1 por causa do ticker report
            result.Write((UInt16)Count);
            foreach (var warehouse in this)
            {
                var time_left = Time_Left(warehouse.RegDate, warehouse.DateEnd);//esta errado, as veze buga a lista

                result.Write(warehouse.IDX);
                result.Write(warehouse.TYPEID);
                result.Write(time_left);
                result.Write(warehouse.C0 ?? 0);
                result.Write(warehouse.C1 ?? 0);
                result.Write(warehouse.C2 ?? 0);
                result.Write(warehouse.C3 ?? 0);
                result.Write(warehouse.C4 ?? 0);
                result.Write((byte)0);
                result.Write(warehouse.Flag ?? 0);
                result.Write(UnixTimeConvert(warehouse.RegDate)); //UNIXTIME campo RegDate
                result.Write(0); //UnKnown
                result.Write(UnixTimeConvert(warehouse.DateEnd)); //UNIXTIME campo EndDate
                result.WriteEmptyBytes(4);
                result.Write((byte)2);
                result.WriteStr(warehouse.UCC_NAME, 16);
                result.WriteEmptyBytes(25);
                result.WriteStr(warehouse.UCC_UNIQE, 9);
                result.Write(warehouse.UCC_STATUS ?? 0);
                result.Write((ushort?)warehouse.UCC_COPY_COUNT ?? 0);
                result.WriteStr(warehouse.UCC_DRAWER, 16);
                result.WriteEmptyBytes(60);
                result.Write(warehouse.C0_SLOT ?? 0);
                result.Write(warehouse.C1_SLOT ?? 0);
                result.Write(warehouse.C2_SLOT ?? 0);
                result.Write(warehouse.C3_SLOT ?? 0);
                result.Write(warehouse.C4_SLOT ?? 0);
                result.Write(warehouse.CLUB_POINT ?? 0);
                result.Write(warehouse.CLUB_SLOT_CANCEL ?? 0);
                result.Write(1);//(CLUB_COUNT) > coloque dentro da classe ;)
                result.Write((UInt32)0);
            }
            return result.GetBytes();
        }

        public byte[] GetClubData()
        {
            //var club = Program.IFFFileManager.ClubData(ItemTypeID);

            var club = this.First();
            var result = new PangyaBinaryWriter();

            int? PowerSlot = club.C0;
            int? ControlSlot = club.C1;
            int? ImpactSlot = club.C2;
            int? SpinSlot = club.C3;
            int? CurveSlot = club.C3;

            result.Write(club.IDX);
            result.Write(club.TYPEID);
            result.Write(club.C0 ?? 0);
            result.Write(club.C1 ?? 0);
            result.Write(club.C2 ?? 0);
            result.Write(club.C3 ?? 0);
            result.Write(club.C4 ?? 0);
            result.Write((ushort?)PowerSlot ?? 0);
            result.Write((ushort?)ControlSlot ?? 0);
            result.Write((ushort?)ImpactSlot ?? 0);
            result.Write((ushort?)SpinSlot ?? 0);
            result.Write((ushort?)CurveSlot ?? 0);

            return result.GetBytes();
        }

        public static uint Time_Left(DateTime? Date, DateTime? EndDate)
        {
            if (Date.HasValue == false || Date?.Ticks == 0 && EndDate.HasValue == false || EndDate?.Ticks == 0)
                return uint.MaxValue; //caso a data for zerada

            var totalDias = (DateTime.Parse(EndDate.ToString()).Subtract(DateTime.Parse(Date.ToString()))).Days;


            return (uint)totalDias;
        }

        public static uint UnixTimeConvert(DateTime? date)
        {
            if (date.HasValue == false || date?.Ticks == 0)
                return 0;

            uint unixTimeStamp;

            DateTime currentTime = (DateTime)date;
            DateTime zuluTime = currentTime.ToUniversalTime();
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            unixTimeStamp = (uint)(zuluTime.Subtract(unixEpoch)).TotalSeconds;
            return unixTimeStamp;
        }
    }
}
