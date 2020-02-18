using Pangya_Season7_GS.Handles_Packet;

namespace UGPangya.API.Handles
{
    public class HandleBase<T> where T : PacketResult, new()
    {
        public HandleBase(Player player)
        {
            Player = player;
            Server = player.Server;

            PacketResult = new T();
            PacketResult.Load(player.CurrentPacket.Reader);
        }

        public Player Player { get; set; }

        public TcpServer Server { get; set; }

        public T PacketResult { get; set; }
    }
}