using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainProgram 
{
    class MidBoss
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;
        float rotation = 0f;
        bool right;
        float distance;
        float oldDistance;
        List<Bullets> bullets = new List<Bullets>();
        public Texture2D bulletTexture;
        public int health = 10;
        public bool isVisible = true;

        public MidBoss(Texture2D newtexture, Vector2 newPosition, float newDistance, Texture2D newBulletTexture)
        {
            texture = newtexture;
            position = newPosition;
            distance = newDistance;
            bulletTexture = newBulletTexture;
            oldDistance = distance;
        }

        public Rectangle BoundingBox // Add this property
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public float shoot = 0;

        public void Update(GameTime gameTime)
        {
            position += velocity;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            if(distance <= 0)
            {
                right = true;
                velocity.X = 4f;
            }
            else if (distance >= oldDistance)
            {
                right = false;
                velocity.X = -4f;
            }
            if (right)
            {
                distance += 1;
            }
            else 
            {
                distance -= 1;
            }
            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot > 1)
            {
                shoot = 0;
                ShootBullets();
            }
            Bullets.UpdateBullets(bullets);
        }

        public void ShootBullets()
        {
            Vector2 startPosition = new Vector2(position.X + (texture.Height / 2) - (bulletTexture.Height / 2), position.Y);
            Vector2[] velocities =
            {
                new Vector2(0, velocity.Y + 6f),
                new Vector2(velocity.X + 1f, velocity.Y + 6f),
                new Vector2(velocity.X - 1f, velocity.Y + 6f)
            };

            Bullets.ShootMultipleBullets(bullets, bulletTexture, startPosition, velocities, 12);
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

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                isVisible = false;
            }
        }
    }
}