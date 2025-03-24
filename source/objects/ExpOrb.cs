
using Microsoft.Xna.Framework;
using spaceShooter;

public class ExpOrb : SpaceObject {
    private float displayTimer = 0f;
    public ExpOrb(Rectangle pos) :base(pos, "exp") {

    }
    public override void Draw()
    {   if(displayTimer >= 0.25f) {
        if(finalPos.X > -finalPos.Width && finalPos.X < 1920 + finalPos.Width && finalPos.Y > -finalPos.Height && finalPos.Y < 1080 + finalPos.Height)
            Main.batch.Draw(Assets.textures[texture], finalPos, null, Color.White, 0f, new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.8f);

        }
    }
    public override void Update() {
        displayTimer+= Main.deltaTime;
        if(displayTimer >= 0.5) {
            displayTimer = 0f;
        }
        if(finalPos.Intersects(new Rectangle((int)Main.mainScreen.player.playerStaticPos.X, (int)Main.mainScreen.player.playerStaticPos.Y, 100, 100))) {
            Main.mainScreen.player.ExpCollect(Main.rn.Next(10, 20));
            kill = true;
        }
        base.Update();
    }
}