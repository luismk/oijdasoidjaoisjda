namespace UGPangya.API.Models
{
    public class ChatGameInfo
    {
        public PlayerPostureEnum Posture { get; set; }

        public PlayerActionEnum LastAction { get; set; }

        public byte[] Animation { get; set; }

        public int Animation_2 { get; set; }

        public string AnimationWithEffects { get; set; }
    }
}