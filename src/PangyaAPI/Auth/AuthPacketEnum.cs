using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Auth
{
    public enum AuthPacketEnum
    {
        SERVER_KEEPALIVE = 0x00,
        SERVER_CONNECT = 0x01,
        LS_PLAYER_DUPLCATE_LOGIN = 0x02
    }

    public enum AuthClientTypeEnum
    {
        LoginServer = 0,
        GameServer = 1,
        MessengerServer = 2
    }

}
