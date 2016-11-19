using UnityEngine;
using System.Collections;


// for the autocomplete!

public class Tags{
    public const string player = "Player";
    public const string canvas = "Canvas";
    public const string untagged = "Untagged";

    public class Scenes
    {
        public const string mainMenu = "MainMenu";
        public const string mainScene = "MainScene";
    }

    public class Axis
    {
        //public const string horizontal = "Horizontal";
        //public const string vertical = "Vertical";
    }

    public class Layers
    {
        public const string player = "Player";
    }

    public class AnimatorParams
    {
    }

    public class PlayerPrefKeys
    {
    }

    public class Options
    {
        public const string SoundLevel = "SoundLevel";
        public const string MusicLevel = "MusicLevel";
    }

    public class ShaderParams
    {
        public static int color = Shader.PropertyToID("_Color");
        public static int emission = Shader.PropertyToID("_EmissionColor");
    }
}