using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_MessengerServer
{
    public enum MessengePacketEnum
    {
        /// <summary>
        /// Envia packet 2F 00 00 00 UIDPlayer 
        /// </summary>
        PLAYER_LOGIN = 0x0012,

        UnIdentified_14 = 0x0014,
        /// <summary>
        /// Envia o canal selecionado pelo player
        /// </summary>
        PLAYER_SELECT_CHANNEL = 0x0023,

        /// <summary>
        /// desconhecido
        /// </summary>
        UnIdentified_16 = 0x0016,
        /// <summary>
        /// requesita procurar um amigo, usando messanger_server
        /// </summary>
        PLAYER_FIND_FRIEND = 0x0017,
        /// <summary>
        /// requista o amigo procurado
        /// </summary>
        PLAYER_REQUEST_FRIEND = 0x0018,

        Unknown_1D = 0x001D,
        Unknown_19 = 0x0019,
        /// <summary>
        /// chat do messenger server
        /// </summary>
        PLAYER_CHAT = 0x001E

    }
}
