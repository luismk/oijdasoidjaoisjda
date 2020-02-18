using System.Collections.Generic;
using UGPangya.API.BinaryModels;
using UGPangya.API.Repository;
using UGPangya.API.Repository.Models;
using UGPangya.API.Tools;

namespace UGPangya.API.Collections
{
    public class MascotCollection : List<Mascot>
    {
        public MascotCollection(Player player)
        {
            _player = player;
            _mascotRepository = new MascotRepository();
            AddRange(_mascotRepository.GetByUID(player.Member_Old.UID));
        }

        private Player _player { get; }
        private MascotRepository _mascotRepository { get; }

        public byte[] GetMascotData()
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[] {0xE1, 0x00});
            result.Write((byte) Count); //Total Caddie

            foreach (var mascot in this)
            {
                result.Write(mascot.MID);
                result.Write(mascot.MASCOT_TYPEID);
                result.WriteEmptyBytes(5);
                result.WriteStr(mascot.MESSAGE, 16);
                result.WriteEmptyBytes(14);
                result.Write(mascot.END_DATE_INT.Value);
                result.Write(mascot.DateEnd?.ToPangyaDateTime());
                result.WriteEmptyBytes(1);
            }

            return result.GetBytes();
        }
    }
}