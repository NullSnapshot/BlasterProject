using BulletBlaster.Game.Entities.Behaviors;
using BulletBlaster.Game.Entities.User;
using Microsoft.Xna.Framework;


namespace BulletBlaster.Code.Entities.Behaviors.Mob
{
    internal class UserControlledBehavior : EntityBehavior
    {

        public UserMovement UsersMovements { get; private set; }


        public UserControlledBehavior(UserMovement userMovement)
        {
            UsersMovements = userMovement;
        }

        public override void Update(GameTime gameTime)
        {
            TargetPosition = UsersMovements.getLocation();
            TargetSpeed = UsersMovements.getSpeed();
        }
    }
}
