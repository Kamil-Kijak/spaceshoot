
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceShooter;

public class Player {
    private float rotation;
    private const float ACCELERATION = 0.05f;
    private const float MAX_SPEED = 7f;
    private float speed = 0;
    private float shieldDuration = 0;
    private float instaKillDuration = 0;
    private int health = 100;
    private int exp = 0;
    private int level = 1;
    public Vector2 playerStaticPos = new(1920 / 2, 1080 / 2);
    public Vector2 playerPosition = new(0, 0);
    private ProgressBar healthBar;
    private ProgressBar levelBar;
    private Text levelText;
    private Item itemToUse;
    private Color color = Color.White;

    public Player() {
        levelBar = new(new Rectangle(0, 1080 - 65, 400, 65), 0, 100, exp, "exp: {0}/{1}", 0.8f);
        healthBar = new(new Rectangle(0, 1080 - 2*65 - 20, 400, 65), 0, 100, health, "hp: {0}/{1}", 0.8f);
        levelText = new(string.Format("LV: {0}", level), new Vector2(200, 860), 1.4f);
    }
    public void Draw() {
        Main.batch.Begin( samplerState: SamplerState.PointClamp);
        Main.batch.Draw(Assets.textures["player"], new Rectangle(1920 / 2, 1080 / 2, 100, 100), null, Color.White, -rotation, new Vector2(8, 8), SpriteEffects.None, 0f);
        Main.batch.Draw(Assets.textures["player"], new Rectangle(1920 / 2, 1080 / 2, 100, 100), null, Color.Multiply(color, 0.4f), -rotation, new Vector2(8, 8), SpriteEffects.None, 0f);
        Main.batch.End();
    }
    public void DrawGui() {
        //Main.batch.DrawString(Assets.fonts["main"], playerPosition.ToString(), new Vector2(0, 0), Color.White);
        healthBar.Draw(Color.Red);
        levelBar.Draw(Color.Green);
        levelText.Draw(Color.White);
        itemToUse?.DrawIcon(new Rectangle(1920 - 100, 1080 - 100, 100, 100));
    }
    public void Update() {
        Vector2 distance = InputControler.mousePos - playerStaticPos;
        rotation = (float)Math.Atan2(distance.X, distance.Y) + (float)(180 * (Math.PI / 180));
        if(InputControler.IsKeyPressed("acceleration")) {
            if(speed < MAX_SPEED) {
                speed += ACCELERATION;
            }
        } else if(InputControler.IsKeyPressed("brake")) {
            if(speed > 0) {
                speed -= 0.2f;
            }
            if(speed < 0) {
                speed = 0;
            }
        }
         else {
            if(speed > 0) {
                speed -= 0.03f;
            }
            if(speed < 0) {
                speed = 0;
            }
            
        }
        if(InputControler.IsKeyClicked("shoot")) {
            GameScreen.PlaySound("shoot", 1f);
            Main.mainScreen.spaceObjects.Add(new Laser("playerLaser", new Rectangle(1920 / 2 + (int)playerPosition.X, 1080 / 2 + (int)playerPosition.Y, 30, 30), rotation, false));
        }
        playerPosition.X -= (float)(Math.Sin(rotation) * speed);
        playerPosition.Y -= (float)(Math.Cos(rotation) * speed);
        if(playerPosition.X < -2500 || playerPosition.X > 2500) {
            playerPosition.X -= (float)(Math.Sin(rotation + (float)(180 * (Math.PI / 180))) * speed);
        }
        if(playerPosition.Y < -2500 || playerPosition.Y > 2500) {
            playerPosition.Y -= (float)(Math.Cos(rotation + (float)(180 * (Math.PI / 180))) * speed);
        }
        if(InputControler.IsKeyClicked("use")) {
            if(ItemToUse != null) {
                itemToUse.Use();
                itemToUse = null;
            }
        }
        if(shieldDuration > 0) {
            shieldDuration-= Main.deltaTime;
            color = Color.Blue;
        }
        if(InstaKillDuration > 0) {
            instaKillDuration-= Main.deltaTime;
            color = Color.Red;
        }
        if(instaKillDuration<= 0 && shieldDuration <= 0)
            color = Color.White;
    }
    public void Hit() {
        if(shieldDuration <= 0) {
            health-= 10;
            healthBar.Current = health;
            if(health <= 0) {
                health = 0;
                healthBar.Current = health;
                Main.mainScreen.GameOver();
            }
        }
    }
    public void ExpCollect(int exp) {
        this.exp+= exp;
        levelBar.Current = this.exp;
        GameScreen.PlaySound("collectExp", 1f);
        if(levelBar.GetProgress() >= 1f) {
            // level up
            GameScreen.PlaySound("levelUp", 1f);
            this.exp = 0;
            level++;
            levelBar = new(new Rectangle(0, 1080 - 65, 400, 65), 0, 100 + 50 * (level - 1), this.exp, "exp: {0}/{1}", 0.8f);
            levelText.DisplayText = string.Format("LV: {0}", level);
            Main.mainScreen.spaceObjectsTemp.Add(new FloatingText(playerPosition + new Vector2(1920 / 2, 1080 / 2), "Level UP", 1.1f, Color.Orange));
        }
    }
    public void MedkitCollect() {
        GameScreen.PlaySound("collectItem", 1f);
        health = 100;
        healthBar.Current = health;
    }
    public void Shield() {
        shieldDuration = 10;
    }
    public void InstaKill() {
        instaKillDuration = 10;
    }
    public int Level {
        get {return level;}
        set {
            level = value;
            levelText.DisplayText = string.Format("LV: {0}", level);
        }
    }
     public Item ItemToUse {
        get {return itemToUse;}
        set {itemToUse = value;}
    }
    public float InstaKillDuration {
        get {return instaKillDuration;}
    }
    public int Health {
        get {return health;}
    }
}