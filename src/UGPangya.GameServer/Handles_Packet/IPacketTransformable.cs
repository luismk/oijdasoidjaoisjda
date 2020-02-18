using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public interface IPacketTransformable
    {
        void Load(PangyaBinaryReader reader);
    }
}