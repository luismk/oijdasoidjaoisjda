using UGPangya.API;
using UGPangya.API.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_Season7_GS.Handles_Packet
{
    public interface IPacketTransformable
    {
        void Load(PangyaBinaryReader reader);
    }
}
