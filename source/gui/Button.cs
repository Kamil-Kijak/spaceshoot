
using Microsoft.Xna.Framework;
using spaceShooter;

public class Button {
    private string texture;
    private Rectangle pos;
    private Text text;
    private SwitchAnimation animation;
    private Rectangle hitBox;
    private int frame;
    public Button(string texture, Rectangle pos, string text, float textScale) {
        this.texture = texture;
        this.text = new(text, new Vector2(pos.X, pos.Y), textScale);
        this.pos = new Rectangle(pos.X - pos.Width / 2, pos.Y - pos.Height / 2,pos.Width, pos.Height);
        animation = new(texture);
        hitBox = this.pos;
    }
    public void Draw(Color color) {
        Main.batch.Draw(Assets.textures[texture], pos, animation.GetFrame(frame), color);
        text.Draw(color);
    }
    public bool Update() {
        if(hitBox.Contains(InputControler.mousePos)) {
            frame = 1;
            if(InputControler.IsMouseButtonClicked()) {
                GameScreen.PlaySound("click", 1f);
                return true;
            }
        } else {
            frame = 0;
        }
        return false;
    }
}