
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using spaceShooter;

public class InputControler {
    public static Dictionary<string, Keys> keys = new Dictionary<string, Keys>(){
        {"use", Keys.E},
        {"acceleration", Keys.W},
        {"brake", Keys.S},
        {"shoot", Keys.Space},
        {"esc", Keys.Escape}
    };
    private static bool[] pressedKeys = new bool[255];
    private static KeyboardState keyboard;
    private static MouseState mouse;
    private static bool mouseButtonPressed = false;
    public static Vector2 mousePos;
    public static void DrawCursor() {
        Main.batch.Begin( samplerState: SamplerState.PointClamp);
        Main.batch.Draw(Assets.textures["cursor"], new Rectangle((int)mousePos.X, (int)mousePos.Y, 50,50), Color.White);
        Main.batch.End();
    }
    public static void Update() {
        keyboard = Keyboard.GetState();
        mouse = Mouse.GetState();
        mousePos = fixedMousePos();
    }
    public static bool IsKeyPressed(string key) {
        return keyboard.IsKeyDown(keys[key]);
    }
    public static bool IsKeyUp(string key) {
        return keyboard.IsKeyUp(keys[key]);
    }
    public static bool IsKeyClicked(string key) {
        if(keyboard.IsKeyDown(keys[key])) {
            if(!pressedKeys[(int)keys[key]]) {
                pressedKeys[(int)keys[key]] = true;
                return true;
            }
        } else {
            pressedKeys[(int)keys[key]] = false;
        }
        return false;
    }
    public static bool IsMouseButtonPressed() {
        return mouse.LeftButton == ButtonState.Pressed;
           
    }
    public static bool IsMouseButtonClicked() {
        if(IsMouseButtonPressed() && !mouseButtonPressed) {
            mouseButtonPressed = true;
            return true;
        }
        if(mouse.LeftButton == ButtonState.Released) {
            mouseButtonPressed = false;
        }
        return false;
    }
    public static Vector2 fixedMousePos() {
        return new Vector2(mouse.Position.X / (Main.windowSize.X / 1920f), mouse.Position.Y / (Main.windowSize.Y / 1080f));
    }
    
}