
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceShooter;

public class Enemy : SpaceObject {
    private float rotation;
    private int life;
    private float speed;
    private float fireCooldown;
    private float fireTime = 0f;
    public Enemy(Rectangle position, float speed, float fireCooldown, int life) :base(position, "enemy") {
        rotation = 0;
        this.life = life;
        this.speed = speed;
        this.fireCooldown = fireCooldown;
    }
    public override void Draw()
    {
        Main.batch.Draw(Assets.textures[texture], finalPos, null, Color.White, -rotation, new Vector2(8, 8), SpriteEffects.None, 0.4f);
    }
    public override void Update()
    {
        Vector2 distance = new Vector2(finalPos.X, finalPos.Y) - Main.mainScreen.player.playerStaticPos;
        rotation = (float)Math.Atan2(distance.X, distance.Y);
        if(Math.Abs(distance.X) + Math.Abs(distance.Y) >= 300) {
            position.X -= (int)(Math.Sin(rotation) * speed);
            position.Y -= (int)(Math.Cos(rotation) * speed);
        }
        fireTime += Main.deltaTime;
        if(fireTime >= fireCooldown) {
            fireTime = 0f;
            if(Math.Abs(distance.X) + Math.Abs(distance.Y) <= 900) {
                GameScreen.PlaySound("shoot", 1f);
                Main.mainScreen.spaceObjectsTemp.Add(new Laser("enemyLaser", new Rectangle(position.X, position.Y, 30, 30), rotation, true));
            }
        }

        base.Update();
    }
    public override bool IsColider(string obj)
    {
        switch (obj)
        {
            case "playerLaser":
                life-= 10 + 1 * Main.mainScreen.player.Level;
                Main.mainScreen.spaceObjectsTemp.Add(new FloatingText(new Vector2(position.X, position.Y), (10 + 1 * Main.mainScreen.player.Level).ToString(), 0.8f, Color.Red));
                if(life <= 0 || Main.mainScreen.player.InstaKillDuration > 0) {
                    if(Main.rn.Next(0, 12) == 1) {
                        if(Main.rn.Next(0, 2) == 1) {
                            Main.mainScreen.spaceObjectsTemp.Add(new Item("skull", new Rectangle(position.X, position.Y, 50, 50), Item.Type.Skull));
                        } else {
                            Main.mainScreen.spaceObjectsTemp.Add(new Item("shield", new Rectangle(position.X, position.Y, 50, 50), Item.Type.Shield));
                        }
                    } else {
                        if(Main.rn.Next(0, 4) == 1) {
                            Main.mainScreen.spaceObjectsTemp.Add(new Medkit(new Rectangle(position.X, position.Y, 50, 50)));
                        } else {
                            Main.mainScreen.spaceObjectsTemp.Add(new ExpOrb(new Rectangle(position.X, position.Y, 50, 50)));
                        }

                    }
                    Main.mainScreen.waveSystem.Enemies--;
                    GameScreen.PlaySound("destroy", 1f);
                    kill = true;
                }
                return true;
        }
        return false;
    }
}