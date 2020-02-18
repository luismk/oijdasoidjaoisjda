using PangyaAPI.BinaryModels;
using PangyaAPI.Repository;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Collections
{
    public class CaddieCollection : List<Caddie>
    {
        private Player _player { get; set; }
        private CaddieRepository _caddieRepository { get; set; }

        public CaddieCollection(Player player)
        {
            _player = player;
            _caddieRepository = new CaddieRepository();
            this.AddRange(_caddieRepository.GetByUID(player.Member_Old.UID));
        }


        public byte[] GetCaddieData()
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0x71, 0x00 });
            result.Write((UInt16)Count); //Total Caddie
            result.Write((UInt16)Count); //Total Caddie

            foreach (var caddie in this)
            {
                result.Write(caddie.CID);
                result.Write(caddie.TYPEID);
                result.Write(caddie.SKIN_TYPEID.Value);
                result.Write(caddie.cLevel);
                result.Write(caddie.EXP);
                result.Write(caddie.RentFlag.Value);
                result.Write((ushort)caddie.DAY_LEFT);
                result.Write((ushort)caddie.SKIN_HOUR_LEFT);
                result.Write((byte)0x00);   
                result.Write((ushort)caddie.TriggerPay);
            }

            return result.GetBytes();
        }


        public byte[] ExpirationNotice()
        {
            var result = new PangyaBinaryWriter();
            var data = this.Where(c => c.RentFlag.Value == 2 && c.DAY_LEFT == 65530).FirstOrDefault();

            if (data != null)
            {
                result.Write(new byte[] { 0xD4, 0x00 });
                result.Write(1); //Total Caddie
                result.Write(data.CID);
                result.Write(data.TYPEID);
                result.Write(data.SKIN_TYPEID.Value);
                result.Write(data.cLevel);
                result.Write(data.EXP);
                result.Write(data.RentFlag.Value);
                result.Write(data.DAY_LEFT);
                result.Write((ushort)data.SKIN_HOUR_LEFT);
                result.Write((byte)0x00);
                result.Write((ushort)data.TriggerPay);
                return result.GetBytes();
            }
            return null;
        }
    }
}
