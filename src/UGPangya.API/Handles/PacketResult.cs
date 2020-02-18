using UGPangya.API.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public abstract class PacketResult : object, IPacketTransformable
    {
        public abstract void Load(PangyaBinaryReader reader);
    }
}