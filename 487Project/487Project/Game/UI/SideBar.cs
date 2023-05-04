using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletBlaster.Game.Entities.User;
using BulletBlaster.Game.Controllers;

namespace BulletBlaster.Game.UI
{
    internal class SideBar
    {
        private SpriteFont Font;
        private UserEntity user;
        private int health;
        private int score;
        private int highScore;

        public SideBar(SpriteFont font, UserEntity user, int highScore)
        {
            this.Font = font;
            this.health = user.health;
            this.score = ScoreSystem.Score;
            this.highScore = highScore;
            this.user = user;
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            //_spriteBatch.DrawString(this.Font, "High Score: " + this.highScore.ToString(), new Vector2(1050, 115), Color.Gray);
            _spriteBatch.DrawString(this.Font, "Entity Count: " + EntityManager.EntityCount, new Vector2(1050, 115), Color.Gray);
            _spriteBatch.DrawString(this.Font, "Score: " + this.score.ToString(), new Vector2(1050, 165), Color.White);
            _spriteBatch.DrawString(this.Font, "Health: " + this.health.ToString(), new Vector2(1050, 215), Color.Red);
        }

        public void update()
        {
            this.score = ScoreSystem.Score;
            this.health = this.user.health;
        }
    }
}
