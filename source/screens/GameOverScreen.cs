
using Microsoft.Xna.Framework;
using spaceShooter;

public class GameOverScreen {
    private bool active;
    private float showAnimation = -0.5f;
    private float recordAnimation = -1f;
    private Text gameOverText;
    private Text waveRecord;
    private Button retry = new ("button", new Rectangle(1920 / 2, 500, 400, 200), "Retry", 1.2f);
    private Button mainMenuButton = new ("button", new Rectangle(1920 / 2, 710, 400, 200), "Main Menu", 1.1f);
    private Button exitButton = new ("button", new Rectangle(1920 / 2, 920, 400, 200), "Exit", 1.3f);
    public GameOverScreen() {
        gameOverText = new("Game Over", new Vector2(1920 / 2, 100), 3f);
        waveRecord = new("No record", new Vector2(1920 / 2, 300), 2f);
    }
    public void Draw() {
        if(active) {
            Main.batch.Draw(Assets.textures["whiteBox"], new Rectangle(0, 0, 1920, 1080), Color.Multiply(Color.Red, 0.3f * showAnimation));
            gameOverText.Draw(Color.Multiply(Color.White, showAnimation));
            retry.Draw(Color.Multiply(Color.White, showAnimation));
            mainMenuButton.Draw(Color.Multiply(Color.White, showAnimation));
            exitButton.Draw(Color.Multiply(Color.White, showAnimation));
            if(recordAnimation > 0) {
                waveRecord.Draw(Color.Multiply(Color.White, showAnimation));
            }
        }
    }
    public void Update() {
        if(active) {
            if(showAnimation < 1) {
                showAnimation+= Main.deltaTime;
            }
            if(exitButton.Update()) {
                Main.run = false;
            }
            if(mainMenuButton.Update()) {
                Main.mainScreen = new();
            }
            if(retry.Update()) {
                Main.mainScreen = new(true);
            }
            recordAnimation+= Main.deltaTime;
            if(recordAnimation >= 1f) {
                recordAnimation = -1;
            }
        }
    }
    public bool Active {
        get {return active;}
        set {
            active = value;
            if(Main.mainScreen.waveSystem.Wave > Main.mainScreen.HighWave) {
                waveRecord.DisplayText = string.Format("New record {0} waves", Main.mainScreen.waveSystem.Wave); 
            }
            Main.mainScreen.SaveHighWave();
        }
    }
}