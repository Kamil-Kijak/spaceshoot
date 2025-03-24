


using Microsoft.Xna.Framework;
using spaceShooter;

public class SpaceObject {
    protected Rectangle position;
    protected Rectangle finalPos;
    protected string texture;
    public bool kill = false;
    public SpaceObject(Rectangle position, string texture) {
        this.position = position;
        this.texture = texture;
        finalPos = new Rectangle(position.X, position.Y, position.Width, position.Height);
    }
    public virtual void Draw() {
        if(finalPos.X > -finalPos.Width && finalPos.X < 1920 + finalPos.Width && finalPos.Y > -finalPos.Height && finalPos.Y < 1080 + finalPos.Height)
            Main.batch.Draw(Assets.textures[texture], finalPos, null, Color.White, 0f, new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
    }
    public virtual void Update() {
        finalPos.X = (int)(position.X - Main.mainScreen.player.playerPosition.X);
        finalPos.Y = (int)(position.Y - Main.mainScreen.player.playerPosition.Y);
    }
    public virtual bool IsColider(string obj) {
        return false;
    }
    public Rectangle Position {
        get { return position; }
        set { position = value; }
    }
}