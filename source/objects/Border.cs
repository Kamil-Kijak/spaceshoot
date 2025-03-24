
using Microsoft.Xna.Framework;
using spaceShooter;

public class Border : SpaceObject {
    public Border(Rectangle position) : base(position, "whiteBox") {

    }
    public override void Draw()
    {
        Main.batch.Draw(Assets.textures[texture], finalPos, null, Color.Multiply(Color.Red, 0.5f), 0f, new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.5f);
    }
}