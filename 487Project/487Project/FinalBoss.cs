using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainProgram 
{
    class FinalBoss
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;
        float rotation = 0f;
        bool right;
        bool up;
        float distanceX;
        float distanceY;
        float oldDistanceX;
        float oldDistanceY;
        List<Bullets> bullets = new List<Bullets>();
        public Texture2D bulletTexture;

        public FinalBoss(Texture2D newtexture, Vector2 newPosition, float newDistanceX, float newDistanceY, Texture2D newBulletTexture)
        {
            texture = newtexture;
            position = newPosition;
            distanceX = newDistanceX;
            distanceY = newDistanceY;
            oldDistanceX = distanceX;
            oldDistanceY = distanceY;
            bulletTexture = newBulletTexture;
        }

        public float shoot = 0;

        public void Update (GameTime gameTime)
        {
            position += velocity;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            if(distanceX <= 0)
            {
                right = true;
                up = true;
                velocity.X = 4f;
                velocity.Y = 4f;
            }
            else if (distanceX >= oldDistanceX)
            {
                right = false;
                up = false;
                velocity.X = -4f;
                velocity.Y = -4f;
            }
            if (right)
            {
                distanceX += 1;
                distanceY += 1;
            }
            else 
            {
                distanceX -= 1;
                distanceY -= 1;
            }
            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot > 1)
            {
                shoot = 0;
                ShootBullets();
            }
            UpdateBullets();
        }
        

        public void UpdateBullets()
        {
            foreach(Bullets bullet in bullets)
            {
                bullet.position += bullet.velocity;
                if(bullet.position.Y > 1200 || bullet.position.X < 90 || bullet.position.X > 950 || bullet.position.Y < 105)
                {
                    bullet.isVisible = false;
                }
            }
            for(int i = 0; i < bullets.Count; i++)
            {
                if(!bullets[i].isVisible)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ShootBullets()
        {
            Bullets newBullet = new Bullets(bulletTexture);
            Bullets newBulletTwo = new Bullets(bulletTexture);
            Bullets newBulletThree = new Bullets(bulletTexture);
            Bullets newBulletFour = new Bullets(bulletTexture);
            newBullet.velocity.Y = velocity.Y + 3f;
            newBulletTwo.velocity.Y = velocity.Y + 6f;
            newBulletTwo.velocity.X = velocity.X + 1f; 
            newBulletThree.velocity.Y = velocity.Y + 4f;
            newBulletThree.velocity.X = velocity.X - .5f;
            newBulletFour.velocity.Y = velocity.Y + 2f;
            newBulletFour.velocity.X = velocity.X - .5f;
            newBullet.position = new Vector2(position.X + (texture.Height / 2) - (bulletTexture.Height/ 2), position.Y + newBullet.velocity.Y);
            newBulletTwo.position = new Vector2(position.X + (texture.Height / 2) - (bulletTexture.Height/ 2), position.Y + newBullet.velocity.Y);
            newBulletThree.position = new Vector2(position.X + (texture.Height / 2) - (bulletTexture.Height/ 2), position.Y + newBullet.velocity.Y);
            newBulletFour.position = new Vector2(position.X + (texture.Height / 2) - (bulletTexture.Height/ 2), position.Y + newBullet.velocity.Y);
            newBullet.isVisible = true;
            newBulletTwo.isVisible = true;
            newBulletThree.isVisible = true;
            newBulletFour.isVisible = true;
            if(bullets.Count() < 21)
            {
                bullets.Add(newBullet);
                bullets.Add(newBulletTwo);
                bullets.Add(newBulletThree);
                bullets.Add(newBulletFour);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
            if(velocity.X > 0)
            {
                spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}