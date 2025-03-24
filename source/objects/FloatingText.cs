
using Microsoft.Xna.Framework;

public class FloatingText : SpaceObject {

    private Text text;
    private Color color;
    private float moveY = 0f;
    public FloatingText(Vector2 pos, string text, float scale, Color color) : base(new Rectangle((int)pos.X, (int)pos.Y, 100, 100), "") {
        base.Update();
        this.text = new(text, new Vector2(finalPos.X, finalPos.Y), scale);
        this.color = color;
    }
    public override void Draw()
    {
        text.Draw(color);
    }
    public override void Update()
    {
        moveY -= 1;
        if(moveY <= -100) {
            kill = true;
        }
        text.Pos = new Vector2(finalPos.X, finalPos.Y + moveY);
        base.Update();
    }
}