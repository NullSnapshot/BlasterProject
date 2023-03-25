using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace MainProgram
{
    public class UserMovement
    {
        Vector2 userPosition;

        public UserMovement(Vector2 userPosition)
        {
            this.userPosition = userPosition;
        }

        public void updateLocation(Vector2 newPosition)
        {
            this.userPosition = newPosition;
        }

        public Vector2 getLocation()
        {
            return this.userPosition;
        }

    }
}
