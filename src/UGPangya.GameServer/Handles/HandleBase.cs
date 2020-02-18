using UGPangya.GameServer.Handles_Packet;
using UGPangya.API;
using PacketResult = UGPangya.GameServer.Handles_Packet.PacketResult;

namespace UGPangya.GameServer.Handles
{
    public class HandleBase<T> where T : PacketResult, new()
    {
        public Player Player { get; set; }

        public GameServerTcp Server { get; set; }

        public T PacketResult { get; set; }

        public HandleBase(Player player)
        {
            this.Player = player;
            this.Server = player.Server as GameServerTcp;

            PacketResult = new T();
            PacketResult.Load(player.CurrentPacket.Reader);
        }
    }
}
