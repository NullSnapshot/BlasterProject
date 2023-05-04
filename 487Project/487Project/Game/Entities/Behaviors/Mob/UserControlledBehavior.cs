using BulletBlaster.Game.Entities.Behaviors;
using BulletBlaster.Game.Entities.User;
using Microsoft.Xna.Framework;


namespace BulletBlaster.Code.Entities.Behaviors.Mob
{
    internal class UserControlledBehavior : EntityBehavior
    {


        public UserControlledBehavior(int targetSpeed, Vector2 startPos)
        {
            this.TargetSpeed = targetSpeed;
            this.TargetPosition = startPos;
        }

        public override void Update(GameTime gameTime)
        {
            // Intentionally blank
        }

        public void SetTargetPosition(Vector2 targetPosition)
        {
            this.TargetPosition = targetPosition;
        }
    }
}
