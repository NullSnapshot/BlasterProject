using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainProgram 
{
    class Enemies
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public bool isVisible = true;

        //Random random = new Random();

        List<Bullets> bullets = new List<Bullets>();
        public Texture2D bulletTexture;

        public Enemies(Texture2D newtexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            texture = newtexture;
            position = newPosition;
            bulletTexture = newBulletTexture;
            velocity = new Vector2(0, 3);
        } 

        public float shoot = 0;
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {
            position += velocity;
            if(position.X <= 0 || position.X >= 1013 || position.X <= 89 + texture.Width)
            {
                velocity.X = -velocity.X;
            }
            if(position.Y > 1266 - texture.Height || position.Y < 104)
            {
                isVisible = false;
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
                if(bullet.position.Y > 1200)
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
            newBullet.velocity.Y = velocity.Y + 6f;
            newBullet.position = new Vector2(position.X + (texture.Height / 2) - (bulletTexture.Height/ 2), position.Y + newBullet.velocity.Y);
            newBullet.isVisible = true;
            if(bullets.Count() < 3)
            {
                bullets.Add(newBullet);
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, position, Color.Red);
        }
    }
}