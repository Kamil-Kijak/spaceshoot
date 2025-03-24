
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using spaceShooter;

public class PauseScreen {
    private bool pause = false;
    private Text pauseText;
    private Button continueButton = new ("button", new Rectangle(1920 / 2, 300, 400, 200), "Continue", 1.2f);
    private Button mainMenuButton = new ("button", new Rectangle(1920 / 2, 500, 400, 200), "Main Menu", 1.1f);
    private Button exitButton = new ("button", new Rectangle(1920 / 2, 700, 400, 200), "Exit", 1.3f);

    public PauseScreen() {
        pauseText = new("Pause", new Vector2(1920 / 2, 100), 2.3f);
    }
    public void Draw() {
        Main.batch.Draw(Assets.textures["whiteBox"], new Rectangle(0, 0, 1920, 1080), Color.Multiply(Color.Black, 0.5f));
        pauseText.Draw(Color.White);
        exitButton.Draw(Color.White);
        continueButton.Draw(Color.White);
        mainMenuButton.Draw(Color.White);
    }
    public void Update() {
        if(InputControler.IsKeyClicked("esc")) {
            MediaPlayer.Resume();
            pause = false;
        }
        if(exitButton.Update()) {
            Main.run = false;
             Main.mainScreen.SaveHighWave();
        }
        if(continueButton.Update()) {
            MediaPlayer.Resume();
            pause = false;
        }
        if(mainMenuButton.Update()) {
            Main.mainScreen.SaveHighWave();
            Main.mainScreen = new();
        }
    }
    public bool Pause {
        get {return pause;}
        set {pause = value;}
    }
}