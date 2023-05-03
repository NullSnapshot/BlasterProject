using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace BulletBlaster.Game.Entities.User
{
    public class UserMovement
    {
        Vector2 userPosition;
        float speed;
        Texture2D texture;

        public UserMovement(Vector2 userPosition, float ballSpeed)
        {
            this.userPosition = userPosition;
            speed = ballSpeed;
        }

        public void updateLocation(Vector2 newPosition)
        {
            userPosition = newPosition;
        }

        public Vector2 getLocation()
        {
            return userPosition;
        }

        public void updateSpeed(float speed)
        {
            this.speed = speed;
        }

        public float getSpeed()
        {
            return speed;
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public Texture2D getTexture()
        {
            return texture;
        }
    }
}
