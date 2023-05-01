using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class UserControlledBehavior : EntityBehavior
    {

        UserMovement usersMovements;


        public UserControlledBehavior(UserMovement userMovement) 
        {
            this.usersMovements = userMovement;
        }

        public override void Update(GameTime gameTime)
        {
            this.TargetPosition = this.usersMovements.getLocation();
            this.TargetSpeed = this.usersMovements.getSpeed();
        }
    }
}
