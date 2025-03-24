
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceShooter;

public class Laser : SpaceObject {
    private Vector2 direction;
    private bool isEmeny;
    private float disposeTimer;
    private float rotation;
    public Laser(string texture, Rectangle pos, float rotation, bool isEmeny) : base(pos, texture) {
        this.rotation = rotation;
        direction = new Vector2(
            (float)Math.Sin(rotation + (float)(180 * (Math.PI / 180))) ,
            (float)Math.Cos(rotation + (float)(180 * (Math.PI / 180))) 
         );
        this.isEmeny = isEmeny;
        disposeTimer = 3f;
    }
    public override void Draw()
    {
        Main.batch.Draw(Assets.textures[texture], finalPos, null, Color.White, -rotation, new Vector2(8, 8), SpriteEffects.None, .5f);
    }
    public override void Update() {
        disposeTimer -= Main.deltaTime;
        if(disposeTimer <= 0) {
            kill = true;
        }
        position.X += (int)(direction.X * 20);
        position.Y += (int)(direction.Y * 20);
        if(!isEmeny) {
            foreach (SpaceObject obj in Main.mainScreen.spaceObjects) {
                if(position.Intersects(obj.Position)) {
                    if(obj.IsColider("playerLaser")) {
                        GameScreen.PlaySound("hit", 1f);
                        kill = true;
                        break;
                    }
                }
            }
        } else {
            if(finalPos.Intersects(new Rectangle((int)Main.mainScreen.player.playerStaticPos.X, (int)Main.mainScreen.player.playerStaticPos.Y, 100, 100))) {
                Main.mainScreen.player.Hit();
                GameScreen.PlaySound("hit", 1f);
                kill = true;
            }
        }
        base.Update();
    }
}