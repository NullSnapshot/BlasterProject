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
    internal class EnemyBulletFactory : BulletFactory
    {
        public EnemyBulletFactory(BulletPatternConfig pattern, Texture2D sprite)
            : base(pattern, sprite)
        {

        }
        public override Bullet Create(Vector2 velocity, Vector2 startPos)
        {
            BulletBehavior newBulletBehavior;
            if (this.config.bullet_type == "homing")
            {
                PlayerTargetBulletBehavior newPlayerTargetBehavior = new PlayerTargetBulletBehavior(startPos, this.config);
                newBulletBehavior = (BulletBehavior)newPlayerTargetBehavior;
            }
            else
            {
                newBulletBehavior = new EnemyBulletBehavior(startPos, this.config);
            }
            EnemyBullet bullet = new EnemyBullet(newBulletBehavior, this.texture, startPos);
            newBulletBehavior.Velocity = velocity;
            EntityManager.RegisterPlayerCollidableEntity(bullet);
            newBulletBehavior.Fire();
            bullet.Damage = newBulletBehavior.Damage;
            return bullet;
        }
    }
}
