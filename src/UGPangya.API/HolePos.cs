namespace UGPangya.API
{
    public class HolePos
    {
        public float X { get; set; }

        public float Z { get; set; }

        public float GetDistance()
        {
            return X + Z;
        }
    }
}