namespace UGPangya.API.Repository.Models
{
    public class PangyaServer
    {
        public int ServerID { get; set; }

        public string Name { get; set; }

        public string IP { get; set; }

        public short Port { get; set; }

        public byte ImgNo { get; set; }

        public short ImgEvent { get; set; }

        public byte ServerType { get; set; }
    }
}