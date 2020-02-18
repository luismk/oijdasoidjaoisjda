using UGPangya.API.BinaryModels;

namespace Pangya_Season7_GS.Handles_Packet
{
    public interface IPacketTransformable
    {
        void Load(PangyaBinaryReader reader);
    }
}