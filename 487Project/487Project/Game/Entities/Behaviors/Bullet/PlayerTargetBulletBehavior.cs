

using BulletBlaster.Game.Controllers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace BulletBlaster.Game.Entities.Behaviors.Bullet
{
    internal class PlayerTargetBulletBehavior : BulletBehavior
    {
        public PlayerTargetBulletBehavior() { }

        public override void Fire()
        {
            this.Velocity = EntityManager.GetPlayerPos().NormalizedCopy() * this.TargetSpeed;
        }
    }
}
