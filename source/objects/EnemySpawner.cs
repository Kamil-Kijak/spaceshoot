
using Microsoft.Xna.Framework;
using spaceShooter;

public class EnemySpawner : SpaceObject {
    private float spawnTimer;
    private float animationTimer = 0f;
    public EnemySpawner(Rectangle position, float timer) :base(position, "spawnPlace") {
        spawnTimer = timer;
    }
    public override void Draw()
    {
        if(animationTimer > 0.25f)
            base.Draw();
    }
    public override void Update() {
        animationTimer+= Main.deltaTime;
        spawnTimer -= Main.deltaTime;
        if(animationTimer > 0.5f) {
            animationTimer = 0;
        }
        if(spawnTimer <= 0) {
            kill = true;
            float fireCooldown = 2f - (Main.mainScreen.waveSystem.Wave / 20f);
            if(fireCooldown < 0.6f) {
                fireCooldown = 0.6f;
            }
            Main.mainScreen.spaceObjectsTemp.Add(new Enemy(position, 5f, fireCooldown, 50 + 20 *  Main.mainScreen.waveSystem.Wave / 7));
        }
        base.Update();
    }
}