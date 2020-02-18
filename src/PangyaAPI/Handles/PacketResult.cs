using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles_Packet
{
    public abstract class PacketResult : Object, IPacketTransformable
    {
        public abstract void Load(PangyaBinaryReader reader);
    }
}
