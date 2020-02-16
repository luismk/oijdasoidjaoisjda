using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_CHANGE_EQUIPMENTS : PacketResult
    {
        public enum ChangeEquipmentEnum : byte
        {
            /// <summary>
            /// Salva a lista de roupas selecionadas 
            /// </summary>
            SetEquip_Char = 0,
            /// <summary>
            /// Seta Index do Caddie
            /// </summary>
            SetIndexCaddie = 1,
            /// <summary>
            /// Salva uma lista de itens do tipo active no inventario
            /// </summary>
            SetItensPlay = 2,
            /// <summary>
            /// Salva index do taco selecionado, e typeID da Bolinha
            /// </summary>
            SetGolfEQP = 3,
            /// <summary>
            /// Salva a decoração do armario, title, painel de fundo etc...
            /// </summary>
            SetDecoration = 4,
            /// <summary>
            ///  Seta Index do Character
            /// </summary>
            SetIndexChar = 5,
            /// <summary>
            ///  Seta Index do Mascot
            /// </summary>
            SetIndexMascot = 8,
            /// <summary>
            ///  Seta Index do Cutin no Character
            /// </summary>
            SetCharCutin = 9
        }

        public ChangeEquipmentEnum Action { get; set; }

        public Character Character { get; set; }

        public int CaddieId { get; set; }

        public int MascoteId { get; set; }


        public override void Load(PangyaBinaryReader reader)
        {
            Action = (ChangeEquipmentEnum)reader.ReadByte();

            switch (Action)
            {
                case ChangeEquipmentEnum.SetEquip_Char:
                    {
                        this.Character = new Character();

                        Character.TYPEID = reader.ReadInt32();
                        Character.CID = reader.ReadInt32();
                    }
                    break;
                case ChangeEquipmentEnum.SetIndexCaddie:
                    {
                        CaddieId = reader.ReadInt32();
                    }
                    break;
                case ChangeEquipmentEnum.SetItensPlay:
                    break;
                case ChangeEquipmentEnum.SetGolfEQP:
                    break;
                case ChangeEquipmentEnum.SetDecoration:
                    break;
                case ChangeEquipmentEnum.SetIndexChar:
                    break;
                case ChangeEquipmentEnum.SetIndexMascot:
                    {
                        MascoteId = reader.ReadInt32();
                    }
                    break;
                case ChangeEquipmentEnum.SetCharCutin:
                    break;
                default:
                    break;
            }
        }
    }
}
