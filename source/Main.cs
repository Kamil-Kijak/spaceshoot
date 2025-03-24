
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace spaceShooter;

public class Main : Game
{
    private GraphicsDeviceManager graph;
    public static SpriteBatch batch;
    private RenderTarget2D target2D;
    public static Random rn = new();
    public static GameScreen mainScreen;
    public static Vector2 windowSize;
    public static string appPath;
    public static float deltaTime;
    public static bool run = true;
    public Main()
    {
        graph = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        windowSize.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        windowSize.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        graph.PreferredBackBufferWidth = (int)windowSize.X;
        graph.PreferredBackBufferHeight = (int)windowSize.Y;
        graph.ToggleFullScreen();
        appPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "spaceShooter");
        if(!Directory.Exists(appPath)) {
            Directory.CreateDirectory(appPath);
        }
        MediaPlayer.IsRepeating = true;
        
    }

    protected override void Initialize()
    {
        Window.Title = "SpaceShoot";

        base.Initialize();
    }

    protected override void LoadContent()
    {
        batch = new SpriteBatch(GraphicsDevice);
        target2D = new RenderTarget2D(GraphicsDevice, 1920, 1080);
        Assets assets= new(Content);
        assets.Load();
        mainScreen = new();

    }

    protected override void Update(GameTime gameTime)
    {
        InputControler.Update();
        deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(!run) {
            Exit();
        }
        if(IsActive) {
            mainScreen.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(target2D);
        GraphicsDevice.Clear(new Color(3, 0, 23));
        // drawing code
        mainScreen.Draw();
        InputControler.DrawCursor();
        
        GraphicsDevice.SetRenderTarget(null);
        batch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null);
        batch.Draw(target2D, GraphicsDevice.Viewport.Bounds, Color.White);
        batch.End();

        base.Draw(gameTime);
    }
}
