
using Microsoft.Xna.Framework;
using spaceShooter;

public class Item : SpaceObject {
    public enum Type : int
    {
        Shield = 0,
        Skull = 1
    }
    private Type type;
    public Item(string texture, Rectangle pos, Type type) : base(pos, texture) {
        this.type = type;
    }
    public override void Draw()
    {
        if(finalPos.X > -finalPos.Width && finalPos.X < 1920 + finalPos.Width && finalPos.Y > -finalPos.Height && finalPos.Y < 1080 + finalPos.Height)
            Main.batch.Draw(Assets.textures[texture], finalPos, null, Color.White, 0f, new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.8f);
        }
    public override void Update()
    {
        if(Main.mainScreen.player.ItemToUse == null) {
            if(finalPos.Intersects(new Rectangle((int)Main.mainScreen.player.playerStaticPos.X, (int)Main.mainScreen.player.playerStaticPos.Y, 100, 100))) {
                Main.mainScreen.player.ItemToUse = this;
                GameScreen.PlaySound("collectItem", 1f);
                kill = true;
            }

        }
        base.Update();
    }
    public void Use() {
        switch(type) {
            case Type.Shield:
                Main.mainScreen.player.Shield();
            break;
            case Type.Skull:
                Main.mainScreen.player.InstaKill();
            break;
        }
    }
    public void DrawIcon(Rectangle pos) {
        Main.batch.Draw(Assets.textures[texture], pos,  Color.White);
    }
}