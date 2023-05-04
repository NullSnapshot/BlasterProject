

using BulletBlaster.Game.Controllers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace BulletBlaster.Game.Entities.Behaviors.Bullet
{
    internal class PlayerTargetBulletBehavior : BulletBehavior
    {
        public PlayerTargetBulletBehavior() { }

        public override void Fire(GameTime gameTime)
        {
            this.velocity = EntityManager.GetPlayerPos().NormalizedCopy() * this.TargetSpeed;
            base.Fire(gameTime);
        }
    }
}
