

using System.Collections.Generic;

namespace BulletBlaster.Game.config
{
    internal class EnemyConfig
    {
        public string enemy_sprite { get; set; }
        public int enemyAmount { get; set; }
        public int offset { get; set; }
        public int maxHealth { get; set; }
        public SpritePosition position { get; set; }
        public EnemyMovement enemyMovement { get; set; }
        public int patternSeed { get; set; }
        public List<BulletPatternConfig>  attackPatterns { get; set; }

        public int attackPatternSeed { get; set; }

    }

    internal class SpritePosition
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    internal class EnemyMovement
    {
        public string movement_type { get; set; }
        public string direction { get; set; }
        public int movement_speed { get; set; }
        public int amplitude { get; set; }
        public int period { get; set; }
    }

    internal class BulletPatternConfig
    {
        public string bullet_sprite { get; set; }
        public string bullet_type { get; set; }
        public string pattern { get; set; }
        public int bullet_count { get; set; }
        public int bullet_speed { get; set; }
        public int damage { get; set; }
    }

}
