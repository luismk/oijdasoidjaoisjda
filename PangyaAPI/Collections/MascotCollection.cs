using PangyaAPI.BinaryModels;
using PangyaAPI.Repository;
using PangyaAPI.Repository.Models;
using PangyaAPI.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Collections
{
    public class MascotCollection : List<Mascot>
    {
        private Player _player { get; set; }
        private MascotRepository _mascotRepository { get; set; }

        public MascotCollection(Player player)
        {
            _player = player;
            _mascotRepository = new MascotRepository();
            this.AddRange(_mascotRepository.GetByUID(player.Member_Old.UID));
        }

        public byte[] GetMascotData()
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0xE1, 0x00 });
            result.Write((byte)Count); //Total Caddie

            foreach (var mascot in this)
            {
                result.Write(mascot.MID);
                result.Write(mascot.MASCOT_TYPEID);
                result.WriteEmptyBytes(5);
                result.WriteStr(mascot.MESSAGE, 16);
                result.WriteEmptyBytes(length: 14);
                result.Write(mascot.END_DATE_INT.Value);
                result.Write(mascot.DateEnd?.ToPangyaDateTime());
                result.WriteEmptyBytes(1);
            }

            return result.GetBytes();
        }
    }
}
