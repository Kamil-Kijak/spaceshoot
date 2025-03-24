
using Microsoft.Xna.Framework;

public class SwitchAnimation {
    private Rectangle rectangle;
    public SwitchAnimation(string texture) {
        rectangle = new Rectangle(0,0, Assets.textures[texture].Width / 2, Assets.textures[texture].Height);
    }
    public Rectangle GetFrame(int frame) {
        return new Rectangle(rectangle.Width * frame, 0, rectangle.Width, rectangle.Height);
    }
}