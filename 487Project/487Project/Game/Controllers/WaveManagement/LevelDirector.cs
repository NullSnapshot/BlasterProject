using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace BulletBlaster.Game.Controllers.WaveManagement
{
    internal class LevelDirector : ISpawnerObserver
    {
        Queue<WaveConfig> waveConfigs = new Queue<WaveConfig>();
        WaveConfig nextWaveConfig;

        List<WaveSpawner> activeSpawns = new List<WaveSpawner>();

        ContentManager contentManager;

        public LevelDirector(List<WaveConfig> waves, ContentManager Content)
        {
            contentManager = Content;
            // Transfer list to queue
            foreach (WaveConfig wave in waves)
            {
                waveConfigs.Enqueue(wave);
            }
            nextWaveConfig = waveConfigs.Peek();
        }

        public void Update(GameTime gameTime)
        {
            // trigger wave spawn
            if(this.nextWaveConfig != null)
            {
                if (gameTime.TotalGameTime.TotalSeconds >= nextWaveConfig.start_time)
                {
                    System.Diagnostics.Debug.WriteLine($"Triggering {nextWaveConfig.name} at {gameTime.TotalGameTime.TotalSeconds} seconds.");
                    WaveSpawner newSpawner = new WaveSpawner(nextWaveConfig, contentManager);
                    activeSpawns.Add(newSpawner);
                    newSpawner.Attach(this);

                    waveConfigs.Dequeue(); // Remove first element
                    if (waveConfigs.Count != 0)
                        nextWaveConfig = waveConfigs.Peek(); // update head ref
                    else if (waveConfigs.Count == 0)
                        this.nextWaveConfig = null;
                }
            }
            

            // Run active spawners
            for (int i = 0; i < activeSpawns.Count; i++)
            {
                activeSpawns[i].Update(gameTime);
            }
        }

        public void UpdateObservers(WaveSpawner subject)
        {
            // Wave is fully spawned, decomission the spawner.
            activeSpawns.Remove(subject);
        }
    }
}
