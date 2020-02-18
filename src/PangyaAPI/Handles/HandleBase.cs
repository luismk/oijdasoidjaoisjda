using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;

namespace PangyaAPI.Handles
{
    public class HandleBase<T> where T : PacketResult, new()
    {
        public Player Player { get; set; }

        public TcpServer Server { get; set; }

        public T PacketResult { get; set; }

        public HandleBase(Player player)
        {
            this.Player = player;
            this.Server = player.Server as TcpServer;

            PacketResult = new T();
            PacketResult.Load(player.CurrentPacket.Reader);
        }
    }
}
