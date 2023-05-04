using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Behaviors.Bullet
{
    internal class EnemyBulletBehavior : BulletBehavior
    {
        public EnemyBulletBehavior() { }

        public EnemyBulletBehavior(Vector2 position, BulletPatternConfig bulletConfig) : base(position, bulletConfig) 
        {
        }
    }
}
