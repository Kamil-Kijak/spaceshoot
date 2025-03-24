
using Microsoft.Xna.Framework;
using spaceShooter;

public class Medkit : SpaceObject {
    public Medkit(Rectangle pos) : base(pos, "medkit") {

    }
    public override void Draw()
    {
         if(finalPos.X > -finalPos.Width && finalPos.X < 1920 + finalPos.Width && finalPos.Y > -finalPos.Height && finalPos.Y < 1080 + finalPos.Height)
            Main.batch.Draw(Assets.textures[texture], finalPos, null, Color.White, 0f, new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.8f);
    }
    public override void Update()
    {
        if(Main.mainScreen.player.Health < 100) {
            if(finalPos.Intersects(new Rectangle((int)Main.mainScreen.player.playerStaticPos.X, (int)Main.mainScreen.player.playerStaticPos.Y, 100, 100))) {
                Main.mainScreen.player.MedkitCollect();
                kill = true;
            }
        }
        base.Update();
    }
}