using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace BulletBlaster
{
    class Bullets
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        public bool isVisible = true;

        public Bullets(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Black);
        }

        public Rectangle BoundingBox // Add this property
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public static void UpdateBullets(List<Bullets> bullets)
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.position += bullet.velocity;
                if (bullet.position.Y > 1200)
                {
                    bullet.isVisible = false;
                }
                if (bullet.position.Y < 104)
                {
                    bullet.isVisible = false;
                }
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].isVisible)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void ShootBullets(List<Bullets> bullets, Texture2D bulletTexture, Vector2 startPosition, Vector2 bulletVelocity, int maxBullets)
        {
            Bullets newBullet = new Bullets(bulletTexture);
            newBullet.velocity = bulletVelocity;
            newBullet.position = startPosition;
            newBullet.isVisible = true;
            if (bullets.Count < maxBullets)
            {
                bullets.Add(newBullet);
            }
        }
        public static void ShootMultipleBullets(List<Bullets> bullets, Texture2D bulletTexture, Vector2 startPosition, Vector2[] velocities, int maxBullets)
        {
            for (int i = 0; i < velocities.Length; i++)
            {
                Bullets newBullet = new Bullets(bulletTexture);
                newBullet.position = startPosition;
                newBullet.velocity = velocities[i];
                newBullet.isVisible = true;

                if (bullets.Count < maxBullets)
                {
                    bullets.Add(newBullet);
                }
            }
        }

    }
}