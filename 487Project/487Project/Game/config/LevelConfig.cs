using System.Collections.Generic;


namespace BulletBlaster.Game.config
{
    internal class LevelConfig
    {
        public PlayerConfig player { get; set; }
        public List<WaveConfig> phases { get; set; }
    }
}
