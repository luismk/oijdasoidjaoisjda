using System;

namespace UGPangya.API.Auth
{
    [Serializable]
    public class AuthPacket
    {
        public AuthPacketEnum Id { get; set; }

        public byte[] Data { get; set; }

        public dynamic Message { get; set; }
    }
}