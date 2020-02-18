using UGPangya.API;
using UGPangya.API.BinaryModels;
using UGPangya.API.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UGPangya.GameServer.Handles_Packet
{
    public abstract class PacketResult : Object, IPacketTransformable
    {
        public abstract void Load(PangyaBinaryReader reader);
    }
}
