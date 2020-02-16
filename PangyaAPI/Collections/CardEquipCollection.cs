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
    public class CardEquipCollection : List<CardEquip>
    {
        private Player _player { get; set; }
        private CardEquipRepository _cardEquipRepository { get; set; }

        public CardEquipCollection(Player player)
        {
            _player = player;
            _cardEquipRepository = new CardEquipRepository();
        }

        public CardEquipCollection(IEnumerable<CardEquip> collection) : base(collection)
        {
        }

        //private void LoadByCharacterId(int characterId)
        //{
        //    AddRange(_cardEquipRepository.GetByCharacterId(characterId));
        //}

        public byte[] MapCard(int characterId)
        {
            var result = new PangyaBinaryWriter();
            var mapcard = this.Where(c => c.CHAR_TYPEID == characterId).ToList();

            foreach (var Cards in mapcard)
            {
                //Cards.CardSlot = new byte[Cards.Slot];

                result.WriteUInt32(0x00); //Cards.CardSlot[0]
                result.WriteUInt32(0x00); //Cards.CardSlot[1]
                result.WriteUInt32(0x00); //Cards.CardSlot[2]
                result.WriteUInt32(0x00); //Cards.CardSlot[3]
                result.WriteUInt32(0x00); //Cards.CardSlot[4]
                result.WriteUInt32(0x00); //Cards.CardSlot[5]
                result.WriteUInt32(0x00); //Cards.CardSlot[6]
                result.WriteUInt32(0x00); //Cards.CardSlot[7]
                result.WriteUInt32(0x00); //Cards.CardSlot[8]
                result.WriteUInt32(0x00); //Cards.CardSlot[9]
            }
            return result.GetBytes();
        }
    }
}
