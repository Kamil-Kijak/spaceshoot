
using System;
using Microsoft.Xna.Framework;
using spaceShooter;

public class WaveSystem {
    private float spawnTimer;
    private float timer = 10f;
    private float waveTextTimer = 1f;
    private bool inWave = false;
    private int wave = 0;
    private int enemies;
    private int totalEnemies;
    private int spawned;
    private ProgressBar waveProgress;
    private Text nextWaveText;
    private Text waveText;
    public WaveSystem() {
        waveProgress = new(new Rectangle(1920 / 2 - 450, 0, 900, 70), 0, 100, 100, "Enemies: {0}%", 1.3f);
        nextWaveText = new(string.Format("Next wave in {0} seconds", 10), new Vector2(1920 / 2, 150), 1.1f);
        waveText = new(string.Format("Wave {0}", 1), new Vector2(1920 / 2, 200), 2f);
    }
    public void Draw() {
        if(waveTextTimer < 1) {
            waveText.Draw(Color.Multiply(Color.White, 1f - waveTextTimer));
        }
        if(inWave) {
            waveProgress.Draw(Color.Green);
        } else {
            nextWaveText.Draw(Color.White);
        }
    }
    public void Update() {
        if(!inWave) {
            timer-= Main.deltaTime;
            if(Math.Floor(timer) <= (int)timer) {
                nextWaveText.DisplayText = string.Format("Next wave in {0} seconds", (int)timer);
            }
            if(timer <= 0) {
                inWave = true;
                waveProgress.Current = 100;
                wave++;
                totalEnemies = Main.rn.Next(1 + wave / 2, wave + 2);
                enemies = totalEnemies;
                spawned = totalEnemies;
                waveProgress.Current = 100;
                spawnTimer = (float)Main.rn.NextDouble() * 2 + 1;
                waveTextTimer = 0;
                waveText.DisplayText = string.Format("Wave {0}", wave);
                GameScreen.PlaySound("newWave", 1f);
            }
        } else {
            if(spawned > 0) {
                spawnTimer-= Main.deltaTime;
                if(spawnTimer <= 0) {
                    spawnTimer = (float)Main.rn.NextDouble() * 4 + 2;
                    spawned--;
                    // spawn new spawner
                    Main.mainScreen.spaceObjects.Add(new EnemySpawner(
                        new Rectangle(Main.rn.Next(-2000, 2000), Main.rn.Next(-2000, 2000), 100, 100)
                        , 3f));
                }
            }
            if(enemies <= 0) {
                inWave = false;
                timer = 10f;
            }
        }
        if(waveTextTimer < 1) {
            waveTextTimer+= Main.deltaTime; 
        }
    }
    public int Wave {
        get {return wave;}
        set {wave = value;}
    }
    public int Enemies {
        get {return enemies;}
        set {
            enemies = value;
            waveProgress.Current = (int)(enemies / (float)totalEnemies * 100);
        }
    }
}