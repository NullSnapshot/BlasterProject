using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class LevelDirector : ISpawnerObserver
    {
        Queue<WaveConfig> waveConfigs = new Queue<WaveConfig>();
        WaveConfig nextWaveConfig;

        List<WaveSpawner> activeSpawns = new List<WaveSpawner>();

        ContentManager contentManager;

        public LevelDirector(List<WaveConfig> waves, ContentManager Content) 
        {
            this.contentManager = Content;
            // Transfer list to queue
            foreach (WaveConfig wave in waves)
            {
                this.waveConfigs.Enqueue(wave);
            }
            this.nextWaveConfig = this.waveConfigs.Peek();
        }

        public void Update(GameTime gameTime)
        {
            // trigger wave spawn
            if (gameTime.TotalGameTime.TotalSeconds >= this.nextWaveConfig.start_time)
            {
                System.Diagnostics.Debug.WriteLine($"Triggering {this.nextWaveConfig.name} at {gameTime.TotalGameTime.TotalSeconds} seconds.");
                WaveSpawner newSpawner = new WaveSpawner(this.nextWaveConfig, contentManager);
                this.activeSpawns.Add(newSpawner);
                newSpawner.Attach(this);
                // END TODO: Wave spawn logic

                this.waveConfigs.Dequeue(); // Remove first element
                if(this.waveConfigs.Count != 0)
                    this.nextWaveConfig = this.waveConfigs.Peek(); // update head ref
            }
            
            // Run active spawners
            for(int i=0; i <this.activeSpawns.Count; i++)
            {
                this.activeSpawns[i].Update(gameTime);
            }
        }

        public void UpdateObservers(WaveSpawner subject)
        {
            // Wave is fully spawned, decomission the spawner.
            this.activeSpawns.Remove(subject);
        }
    }
}
