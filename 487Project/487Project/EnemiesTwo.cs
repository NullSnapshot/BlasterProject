using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainProgram 
{
    class EnemiesTwo
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        public bool isVisible = true;

        //Random random = new Random();

        List<Bullets> bullets = new List<Bullets>();
        Texture2D bulletTexture;

        public EnemiesTwo(Texture2D newtexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            texture = newtexture;
            position = newPosition;
            bulletTexture = newBulletTexture;
            velocity = new Vector2(2, 0);
        } 

        public float shoot = 0;
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {
            position += velocity;
            if(position.Y <= 1266 - texture.Height || position.Y >= 104)
            {
                velocity.Y = -velocity.Y;
            }
            if(position.X > 1013 - texture.Width || position.X < 89)
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
            for(int i = 0; i <bullets.Count; i++)
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
            newBullet.velocity.Y = velocity.Y + 7f;
            newBullet.position = new Vector2(position.X + newBullet.velocity.X, position.Y + (texture.Height / 2) - (bulletTexture.Height/ 2));
            newBullet.isVisible = true;
            if(bullets.Count() < 6)
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