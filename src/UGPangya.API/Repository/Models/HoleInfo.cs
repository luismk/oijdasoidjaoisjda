namespace UGPangya.API.Repository.Models
{
    public class HoleInfo
    {
        public Game GameInfo { get; set; }

        public int Index { get; set; }
        public byte Hole { get; set; }
        public GameModeTypeEnum ModeType { get; set; }
        public ushort Weather { get; set; }
        public ushort WindPower { get; set; }
        public ushort WindDirection { get; set; }
        public byte Course { get; set; }
        public byte Pos { get; set; }
    }
}