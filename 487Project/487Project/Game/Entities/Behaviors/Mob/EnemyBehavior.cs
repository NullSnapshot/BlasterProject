using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Behaviors.Mob
{
    internal abstract class EnemyBehavior : EntityBehavior
    {
        public EnemyConfig SourceConfig;
        public EnemyBehavior() { }

        public EnemyBehavior(EnemyConfig behavior)
        {
            this.TargetPosition = new Vector2(behavior.position.x, behavior.position.y);
            this.TargetSpeed = behavior.enemyMovement.movement_speed;
            this.SourceConfig = behavior;
        }

        public override abstract void Update(GameTime gameTime);
    }
}
