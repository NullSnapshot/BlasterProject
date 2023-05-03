

namespace BulletBlaster.Game.config
{
    internal class EnemyConfig
    {
        public string enemy_sprite { get; set; }
        public string enemyType { get; set; }
        public int enemyAmount { get; set; }
        public int offset { get; set; }
        public int maxHealth { get; set; }
        public SpriteSize size { get; set; }
        public SpritePosition position { get; set; }
        public EnemyMovement enemyMovement { get; set; }
        public EnemyBulletType enemyBulletType { get; set; }

    }

    internal class SpriteSize
    {
        public int width { get; set; }
        public int height { get; set; }

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

    internal class EnemyBulletType
    {
        public string color { get; set; }
        public string bullet_sprite { get; set; }
        public string bullet_type { get; set; }
        public string bullet_movement { get; set; }
        public int bullet_count { get; set; }
        public int bullet_speed { get; set; }
        public int damage { get; set; }
    }

}
