using BulletBlaster.Game.config;
using BulletBlaster.Game.Controllers;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Bullet.Factories
{
    internal class PlayerBulletFactory : BulletFactory
    {
        public PlayerBulletFactory(BulletPatternConfig pattern, Texture2D sprite)
            : base(pattern, sprite)
        {

        }

        public override Bullet Create(Vector2 velocity, Vector2 startPos)
        {
            BulletBehavior newBulletBehavior = new EnemyBulletBehavior(startPos, this.config);
            PlayerBullet bullet = new PlayerBullet(newBulletBehavior, this.texture, startPos);
            newBulletBehavior.Velocity = velocity;
            EntityManager.RegisterPlayerBullet(bullet);
            newBulletBehavior.Fire();
            bullet.Damage = newBulletBehavior.Damage;
            return bullet;
        }
    }
}
