
using Microsoft.Xna.Framework;
using spaceShooter;

public class ControlTip : SpaceObject {
    private float dispear = 9f;
    private Text[] texts;
    public ControlTip(Rectangle pos): base( pos, "") {
        texts = new Text[3]{
            new("W/S - accelerate/brake", new Vector2(finalPos.X, finalPos.Y), 1f),
            new("E - use item", new Vector2(finalPos.X, finalPos.Y + 50), 1f),
            new("Space - SHOOT!!!", new Vector2(finalPos.X, finalPos.Y + 100), 1f)
        };
    }
    public override void Draw()
    {
        if(dispear != 9f) {
            foreach (Text text in texts) {
                text.Draw(Color.Multiply(Color.White, dispear));
            }
        } 
    }
    public override void Update()
    {
        for(int i = 0;i<3;i++) {
            texts[i].Pos = new Vector2(finalPos.X, finalPos.Y + i*50);
        }
        dispear -= Main.deltaTime;
        if(dispear <= 0) {
            kill = true;
        }
        base.Update();
    }
}