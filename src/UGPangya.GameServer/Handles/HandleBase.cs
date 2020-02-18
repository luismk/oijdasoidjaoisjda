using UGPangya.API;
using UGPangya.GameServer.Handles_Packet;

namespace UGPangya.GameServer.Handles
{
    public class HandleBase<T> where T : PacketResult, new()
    {
        public HandleBase(Player player)
        {
            Player = player;
            Server = player.Server as GameServerTcp;

            PacketResult = new T();
            PacketResult.Load(player.CurrentPacket.Reader);
        }

        public Player Player { get; set; }

        public GameServerTcp Server { get; set; }

        public T PacketResult { get; set; }
    }
}