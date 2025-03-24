
using Microsoft.Xna.Framework;
using spaceShooter;

public class Star : SpaceObject {
    public Star(Rectangle position) : base(position, "star") {

    }
    public override void Draw()
    {
        if(finalPos.X > -finalPos.Width && finalPos.X < 1920 + finalPos.Width && finalPos.Y > -finalPos.Height && finalPos.Y < 1080 + finalPos.Height)
            Main.batch.Draw(Assets.textures[texture], finalPos, null, Color.White, 0f, new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1f);
    }
}