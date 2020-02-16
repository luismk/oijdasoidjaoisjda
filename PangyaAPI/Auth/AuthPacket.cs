using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Auth
{
    [Serializable]
    public class AuthPacket
    {
        public AuthPacketEnum Id { get; set; }

        public byte[] Data { get; set; }

        public dynamic Message { get; set; }
    }
}
