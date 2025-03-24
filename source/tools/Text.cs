
using Microsoft.Xna.Framework;
using spaceShooter;

public class Text {
    private string displayText;
    private Vector2 pos;
    private float scale;
    private Vector2 textSize;
    public Text(string text, Vector2 pos, float scale) {
        displayText = text;
        this.pos = pos;
        this.scale = scale;
        textSize = Assets.fonts["main"].MeasureString(text);
    }
    public void Draw(Color color) {
        Main.batch.DrawString(Assets.fonts["main"], displayText, pos, color, 0f, textSize / 2, scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
    }
    public string DisplayText {
        get { return displayText; }
        set {
             displayText = value; 
             textSize = Assets.fonts["main"].MeasureString(displayText);
        }
    }
    public Vector2 Pos {
        get { return pos; }
        set { pos = value; }
    }
}