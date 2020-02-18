using PangyaAPI.BinaryModels;
using PangyaAPI.Repository;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PangyaAPI.Collections
{
    public class CharacterCollection : List<Character>
    {
        private Player _player { get; set; }
        private CharacterRepository _characterRepository { get; set; }

        public CharacterCollection(Player player)
        {
            _player = player;
            _characterRepository = new CharacterRepository();
            Load();
        }

        private void Load()
        {
            AddRange(_characterRepository.GetByUid(_player.Member_Old.UID));
        }

        /// <summary>
        /// Obtém os dados de todos os characters
        /// </summary>
        /// <returns></returns>
        public byte[] GetCharacterData()
        {
            var result = new PangyaBinaryWriter(new MemoryStream());

            result.Write(new byte[] { 0x70, 0x00 });
            result.Write((ushort)Count); //Total Character
            result.Write((ushort)Count); //Total Character

            foreach (var character in this)
            {
                result.Write(GetCharacterData(character.CID));
            }

            return result.GetBytes();
        }

        /// <summary>
        /// Obtém os dados de um character pelo Id
        /// </summary>
        public byte[] GetCharacterData(int characterId)
        {
            var character = this.First(c => c.CID == characterId);

            var result = new PangyaBinaryWriter();
            result.Write(character.TYPEID);
            result.Write(character.CID);
            result.Write(character.HAIR_COLOR);
            result.Write(character.GIFT_FLAG);
            for (int i = 0; i < 24; i++)
            {
                var valorPropriedade = character.CharacterEquip.GetType().GetProperty($"PART_TYPEID_{i + 1}").GetValue(character.CharacterEquip, null);
                result.Write(Convert.ToUInt32((valorPropriedade)));
            }
            for (int i = 0; i < 24; i++)
            {
                var valorPropriedade = character.CharacterEquip.GetType().GetProperty($"PART_IDX_{i + 1}").GetValue(character.CharacterEquip, null);
                result.Write(Convert.ToUInt32((valorPropriedade)));
            }
            result.WriteEmptyBytes(216);
            result.Write(0); //character.AuxPart (UINT) 1879113856
            result.Write(0); //character.AuxPart2 (UINT) 1881210881
            result.WriteEmptyBytes(12); //???
            result.Write(character.CUTIN);
            result.WriteEmptyBytes(12); //????
            result.Write(character.POWER.Value);
            result.Write(character.CONTROL.Value);
            result.Write(character.IMPACT.Value);
            result.Write(character.SPIN.Value);
            result.Write(character.CURVE.Value);
            result.Write((byte)100); //character.MasteryPoint
            result.WriteEmptyBytes(3);
            result.Write(character.CardEquipCollection.MapCard(character.CID), 40); //MAP CARD DATA
            result.Write((UInt32)0);
            result.Write((UInt32)0);

            return result.GetBytes();
        }

        public byte[] GetCurrentCharacterData()
        {
            return GetCharacterData(_player.GetCurrentCharacter().CID);
        }

        public static byte[] GetZero(int length)
        {
            return new byte[length];
        }

        public byte[] GetCharData()
        {
            //var character = this.FirstOrDefault();
            var character = _player.GetCurrentCharacter();

            if (character == null)
            {
                return GetZero(513);
            }

            var result = new PangyaBinaryWriter();

            result.Write(character.TYPEID);
            result.Write(character.CID);
            result.Write((ushort)character.HAIR_COLOR);
            result.Write((ushort)character.GIFT_FLAG);
            for (int i = 0; i < 24; i++)
                {
                    var valorPropriedade = character.CharacterEquip.GetType().GetProperty($"PART_TYPEID_{i + 1}").GetValue(character.CharacterEquip, null);
                    result.Write(Convert.ToUInt32((valorPropriedade)));
                }
                for (int i = 0; i < 24; i++)
                {
                    var valorPropriedade = character.CharacterEquip.GetType().GetProperty($"PART_IDX_{i + 1}").GetValue(character.CharacterEquip, null);
                    result.Write(Convert.ToUInt32((valorPropriedade)));
                }
            result.WriteEmptyBytes(0xD8);
            result.Write(0); //FLRingTypeID
            result.Write(0); //FRRingTypeID
            result.WriteEmptyBytes(0x0C); //???
            result.Write(character.CUTIN);
            result.WriteEmptyBytes(0x0C); //????
            result.Write(character.POWER.Value);
            result.Write(character.CONTROL.Value);
            result.Write(character.IMPACT.Value);
            result.Write(character.SPIN.Value);
            result.Write(character.CURVE.Value);
            result.Write(0x00); //character.MasteryPoint
            result.WriteEmptyBytes(3);
            result.Write(character.CardEquipCollection.MapCard(character.CID), 40); //MAP CARD DATA
            result.Write((UInt32)0);
            result.Write((UInt32)0);

            return result.GetBytes();
        }
    }
}
