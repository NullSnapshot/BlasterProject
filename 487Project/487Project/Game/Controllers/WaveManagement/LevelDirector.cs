using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class LevelDirector
    {
        Queue<WaveConfig> waveConfigs = new Queue<WaveConfig>();
        WaveConfig nextWaveConfig;

        public LevelDirector(List<WaveConfig> waves) 
        {

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
                // TODO: Wave spawn logic
                System.Diagnostics.Debug.WriteLine($"Triggering {this.nextWaveConfig.name} at {gameTime.TotalGameTime.TotalSeconds} seconds.");
                // END TODO: Wave spawn logic

                this.waveConfigs.Dequeue(); // Remove first element
                if(this.waveConfigs.Count != 0)
                    this.nextWaveConfig = this.waveConfigs.Peek(); // update head ref
            }
        }
    }
}
