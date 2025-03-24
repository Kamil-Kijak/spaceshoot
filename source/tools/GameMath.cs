
using Microsoft.Xna.Framework;
using spaceShooter;

public static class GameMath {
    public static Rectangle FixedRectangle(Rectangle rect) {
        return new Rectangle(
            (int)(rect.X * (Main.windowSize.X / 1920f)),
            (int)(rect.Y * (Main.windowSize.Y / 1080f)),
            (int)(rect.Width * (Main.windowSize.X / 1920f)),
            (int)(rect.Height * (Main.windowSize.Y / 1080f))
            );
    }
}