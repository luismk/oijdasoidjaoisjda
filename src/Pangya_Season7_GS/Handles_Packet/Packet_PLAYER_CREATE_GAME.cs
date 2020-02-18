using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository.Models;

namespace Pangya_Season7_GS.Handles_Packet
{
    public class Packet_PLAYER_CREATE_GAME : PacketResult
    {
        public Game Game { get; set; }

        public override void Load(PangyaBinaryReader reader)
        {
            Game = new Game()
            {
                Un = reader.ReadByte(),
                TimeSec = reader.ReadUInt32(),
                TimeMin = reader.ReadUInt32(),
                MaxPlayers = reader.ReadByte(),
                Mode = (GameTypeEnum)reader.ReadByte(),//ok
                Holes = reader.ReadByte(),//
                Course = (GameMapTypeEnum)reader.ReadByte(),
                HoleOrder = (GameModeTypeEnum)reader.ReadByte(),
                NaturalMode = reader.ReadUInt32(),

                RoomTitle = reader.ReadPStr(),
                Password = reader.ReadPStr(),
                Artifact = reader.ReadUInt32()
            };

            Game.BuildCreateHole();
        }
    }
}
