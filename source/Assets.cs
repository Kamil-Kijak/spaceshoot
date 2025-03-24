
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

public class Assets {
    private readonly ContentManager content;
    public static Dictionary<string, Texture2D> textures = new();
    public static Dictionary<string, SoundEffect> sounds = new();
    public static Dictionary<string, SpriteFont> fonts = new();
    public static Dictionary<string, Song> music = new();

    private Dictionary<string, string> texturesToLoad = new Dictionary<string, string>{
        {"cursor", "textures/guiElements/cursor"},
        {"title", "textures/guiElements/title"},
        {"button", "textures/guiElements/button"},
        {"player", "textures/objects/player"},
        {"star", "textures/objects/star"},
        {"progressBarElement", "textures/guiElements/progressBarElement"},
        {"whiteBox", "textures/guiElements/box"},
        {"enemyLaser", "textures/objects/laserEnemy"},
        {"playerLaser", "textures/objects/laserPlayer"},
        {"spawnPlace", "textures/objects/spawnPlace"},
        {"enemy", "textures/objects/enemy"},
        {"exp", "textures/objects/exp"},
        {"medkit", "textures/objects/medkit"},
        {"shield", "textures/objects/shield"},
        {"skull", "textures/objects/skull"},
    };
    private Dictionary<string, string> soundsToLoad = new Dictionary<string, string>{
        {"click", "sounds/click"},
        {"collectExp", "sounds/collect"},
        {"collectItem", "sounds/collectitem"},
        {"gameover", "sounds/gameover"},
        {"hit", "sounds/hit"},
        {"levelUp", "sounds/levelup"},
        {"shoot", "sounds/shoot"},
        {"destroy", "sounds/destroy"},
        {"newWave", "sounds/newwave"}
    };
    private Dictionary<string, string> fontsToLoad = new Dictionary<string, string>{
        {"main", "fonts/main"}
    };
    private Dictionary<string, string> musicToLoad = new Dictionary<string, string>{
        {"mainBeat", "music/beat"}
    };
    public Assets(ContentManager content) {
        this.content = content;
    }
    public void Load() {
        foreach (string key in texturesToLoad.Keys) {
            textures[key] = content.Load<Texture2D>(texturesToLoad[key]);
        }
        foreach (string key in soundsToLoad.Keys) {
            sounds[key] = content.Load<SoundEffect>(soundsToLoad[key]);
        }
        foreach (string key in fontsToLoad.Keys) {
            fonts[key] = content.Load<SpriteFont>(fontsToLoad[key]);
        }
        foreach (string key in musicToLoad.Keys) {
            music[key] = content.Load<Song>(musicToLoad[key]);
        }
    }
}