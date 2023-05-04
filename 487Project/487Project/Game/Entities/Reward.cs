using BulletBlaster.Game.Entities.User;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BulletBlaster.Game.Entities
{
    internal class Reward
    {
        public Texture2D rewardTexture { get; set; }
        public Vector2 rewardPosition { get; set; }

        public TimeSpan spawnTime { get; set; }

        public TimeSpan despawnTime { get; set; }

        private bool visible;


        public Rectangle rectangle { get; set; }

        public UserEntity user;

        public bool collected;


        public Reward(Texture2D rewardTexture, Vector2 rewardPosition, TimeSpan spawnTime, TimeSpan despawnTime, UserEntity user)
        {
            this.rewardTexture = rewardTexture;
            this.rewardPosition = rewardPosition;
            this.spawnTime = spawnTime;
            visible = false;
            this.despawnTime = despawnTime;
            rectangle = new Rectangle((int)rewardPosition.X, (int)rewardPosition.Y, rewardTexture.Width, rewardTexture.Height);
            this.user = user;
            collected = false;
        }

        public void Draw(SpriteBatch sb)
        {
            if (visible)
            {
                sb.Draw(
                rewardTexture,
                rewardPosition,
                null,
                Color.White,
                0f,
                new Vector2(rewardTexture.Width / 2, rewardTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            }
        }
        public void Update(GameTime gameTime)
        {
            if (spawnTime <= gameTime.TotalGameTime && collected == false)
            {
                visible = true;
            }

            if (despawnTime <= gameTime.TotalGameTime && collected == false)
            {
                visible = false;
            }
        }
    }
}
