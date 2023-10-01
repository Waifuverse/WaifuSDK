using System.Collections.Generic;
using UnityEngine;

public enum VoiceType { Female1, Male1 }
public enum VoiceStyle { Soft, Default, Fast, Projected }

[System.Serializable]
public class WaifuInfo : MonoBehaviour
{
    
    
    public Texture2D Picture;
    public string gender = "she";
    public string name = "waifu";
    public string relationship = "girlfriend";
    public List<string> trait = new List<string>() { "Direct", "Enthusiastic" , "Even-tempered" };
    public string BackgroundInfo = " a cheerful and energetic waifu who loves to have fun and make people smile. She's a digital creation, designed to be the perfect companion for her owner, but her personality has grown far beyond her original programming.";


    [Header("Voice, style, speed (50-200), pitch (25-400), gain (1-200)")]
    [Tooltip("Voice, style, speed (50-200), pitch (25-400), gain (1-200)")]
    public VoiceType voiceType = VoiceType.Female1;
    public VoiceStyle voiceStyle = VoiceStyle.Default;
    public WitVoice voice = new WitVoice();

    [Range(50, 200)] public int speed = 115;
    [Range(25, 400)] public int pitch = 135;
    [Range(1, 200)] public int gain = 95;




    public class WitVoice
    {
        public string voice;
        public string style;

        public WitVoice()
        {
            voice = VoiceType.Female1.ToString();
            style = VoiceStyle.Default.ToString();
        }
        //on validate 
    } 
    //on validate 
    
    

}