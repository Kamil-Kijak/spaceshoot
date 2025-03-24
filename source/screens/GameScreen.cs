
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using spaceShooter;

public class GameScreen {
    private bool startMenu = true;
    private int highWave;
    private float highWaveTimer = -1f;
    private bool playButtonClicked = false;
    private float startTimer = 0f;
    private Text highWaveText;
    private Button playButton = new("button", new Rectangle(1920 / 2, 600, 400, 200), "Play", 1.5f);
    private Button exitButton = new("button", new Rectangle(1920 / 2, 810, 400, 200), "Exit", 1.5f);
    public Player player;
    public WaveSystem waveSystem;
    public List<SpaceObject> spaceObjects = new();
    public List<SpaceObject> spaceObjectsTemp = new();
    private Random rn = new Random();
    private PauseScreen pauseScreen;
    private GameOverScreen gameOverScreen;
    public GameScreen(bool start = false) {
        playButtonClicked = start;
        pauseScreen = new();
        gameOverScreen = new();
        waveSystem = new();
        if(File.Exists(Path.Combine(Main.appPath, "save.txt"))) {
            using FileStream fs = new FileStream(Path.Combine(Main.appPath, "save.txt"), FileMode.Open, FileAccess.Read);
            using StreamReader sr = new StreamReader(fs);
            highWave = int.Parse(sr.ReadLine());
        } else {
            highWave = 0;
        }
        highWaveText = new(string.Format("High wave: {0}", highWave), new Vector2(1920 / 2, 450), 1.8f);
        player = new Player();
        // star generate
        for (int i = -3000; i <= 3000 + 1920; i+=125) {
            for (int j = -3000; j <= 3000 + 1080; j+=125) {
                spaceObjects.Add(new Star(new Rectangle(i + (int)(rn.NextDouble() * 100), j + (int)(rn.NextDouble() * 100), 15, 15)));
            }
        }
        // border Generate
        spaceObjects.Add(new Border(new Rectangle(-2500, -2500, 6000 + 1920, 1080 / 2)));
        spaceObjects.Add(new Border(new Rectangle(-2500, 2500 + 1080 / 2, 6000 + 1920, + 1080 / 2)));
        spaceObjects.Add(new Border(new Rectangle(-2500, -2500 + 1080 / 2 + 1, 1920 / 2, 5000 -2)));
        spaceObjects.Add(new Border(new Rectangle(2500 + 1920 / 2, -2500 + 1080 / 2 + 1, 1920 / 2, 5000-2)));
        spaceObjects.Add(new ControlTip(new Rectangle(1920 / 2, 1080 / 2, 100, 100)));

    }
    public void Draw() {
        Main.batch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);
        foreach (SpaceObject obj in spaceObjects) {
            obj.Draw();
        }
        Main.batch.End();
        player.Draw();
        Main.batch.Begin(samplerState: SamplerState.PointClamp);
        if(startMenu) {
            Main.batch.Draw(Assets.textures["title"], new Rectangle(1920 / 2, 200, 900, 360), null, Color.Multiply(Color.White, 1f - startTimer), 0f, new Vector2(40, 16), SpriteEffects.None, 1f);
            if(highWaveTimer > 0) {
                highWaveText.Draw(Color.Multiply(Color.White, 1f - startTimer));
            }
            playButton.Draw(Color.Multiply(Color.White, 1f - startTimer));
            exitButton.Draw(Color.Multiply(Color.White, 1f - startTimer));
            Main.batch.DrawString(Assets.fonts["main"], "Creator: Kamil Kijak", new Vector2(0, 1020),Color.Multiply(Color.White, 1f - startTimer));
        } else {
            // main game
            player.DrawGui();
            waveSystem.Draw();
            gameOverScreen.Draw();
        }
        if(pauseScreen.Pause) {
            pauseScreen.Draw();
        }
        Main.batch.End();

    }
    public void Update() {
        if(startMenu) {
            if(!playButtonClicked) {
                highWaveTimer += Main.deltaTime;
                if(highWaveTimer > 1) {
                    highWaveTimer = -1;
                }
                if(exitButton.Update()) {
                    Main.run = false;
                }
                if(playButton.Update()) {
                    playButtonClicked = true;
                }
            }
            if(playButtonClicked) {
                    startTimer += Main.deltaTime;
            }
            if(startTimer >= 1) {
                startMenu = false;
                MediaPlayer.Play(Assets.music["mainBeat"]);
            }
        } else if(!gameOverScreen.Active) {
            if(pauseScreen.Pause) {
                pauseScreen.Update();
            } else {
                if(InputControler.IsKeyClicked("esc")) {
                    pauseScreen.Pause = true;
                    MediaPlayer.Pause();
                }
                // main game
                foreach (SpaceObject obj in spaceObjectsTemp) {
                    spaceObjects.Add(obj);
                }
                spaceObjectsTemp.Clear();
                waveSystem.Update();
                player.Update();
                foreach (SpaceObject obj in spaceObjects) {
                    obj.Update();
                }
                // kill objects
                spaceObjects.RemoveAll(obj => obj.kill);
            }
        } else {
            gameOverScreen.Update();
        }

    }
    public void GameOver() {
        PlaySound("gameover", 1f);
        MediaPlayer.Stop();
        gameOverScreen.Active = true;
    }
     public void SaveHighWave() {
        if(Main.mainScreen.waveSystem.Wave > Main.mainScreen.HighWave) {
            using FileStream fs = new FileStream(Path.Combine(Main.appPath, "save.txt"), FileMode.Create, FileAccess.Write);
            using StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(Main.mainScreen.waveSystem.Wave);
        }
    }
    public static void PlaySound(string sound, float volume) {
        Assets.sounds[sound].Play(volume, 1f, 1f);
    }
    public int HighWave {
        get {return highWave;}
        set {highWave = value;}
    }
}