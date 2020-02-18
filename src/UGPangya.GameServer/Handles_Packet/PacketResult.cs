using UGPangya.API.BinaryModels;

namespace UGPangya.GameServer.Handles_Packet
{
    public abstract class PacketResult : object, IPacketTransformable
    {
        public abstract void Load(PangyaBinaryReader reader);
    }
}