
using Microsoft.Xna.Framework;
using spaceShooter;

public class ProgressBar {
    private Rectangle position;
    private int min;
    private float max;
    private float current;
    private Text text;
    private string formatString;
    public ProgressBar(Rectangle position, int min, int max, int current, string text, float scale) {
        formatString = text;
        this.position = position;
        this.min = min;
        this.max = max;
        this.current = current;
        this.text = new(string.Format(formatString, current, max), new Vector2(position.X + position.Width / 2, position.Y + position.Height / 2), scale);
    }
    public float GetProgress() {
        return (current - min) / max;
    }
    public void Draw(Color color) {
        Main.batch.Draw(Assets.textures["progressBarElement"], new Rectangle(position.X, position.Y, (int)(position.Width * (current - min) / max), position.Height), color);
        text.Draw(Color.White);
    }
    public float Current
    {
        get {return current;} 
        set {
            current = value;
            text.DisplayText = string.Format(formatString, current, max);
            
        }
    }
}