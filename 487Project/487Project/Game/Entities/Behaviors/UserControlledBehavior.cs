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

        public UserMovement UsersMovements { get; private set; }


        public UserControlledBehavior(UserMovement userMovement) 
        {
            this.UsersMovements = userMovement;
        }

        public override void Update(GameTime gameTime)
        {
            this.TargetPosition = this.UsersMovements.getLocation();
            this.TargetSpeed = this.UsersMovements.getSpeed();
        }
    }
}
