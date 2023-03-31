using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Net.Mime;
using MainProgram;

namespace mainProgram
{
    internal class SideBar
    {
        SpriteFont testFont;
        UserSprite user;
        int health;
        int score;
        int highScore;
        
        public SideBar(SpriteFont font, UserSprite user, int highScore)
        {
            this.testFont = font;
            this.health = user.health;
            this.score = user.score;
            this.highScore = highScore;
            this.user = user;
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(testFont, "High Score: " + highScore.ToString(), new Vector2(1050, 115), Color.Black);
            _spriteBatch.DrawString(testFont, "Score: " + score.ToString(), new Vector2(1050, 165), Color.White);
            _spriteBatch.DrawString(testFont, "Health: " + health.ToString(), new Vector2(1050, 215), Color.Red);
        }

        public void update()
        {
            this.score = user.score;
            this.health = user.health;
            System.Diagnostics.Debug.WriteLine(this.score.ToString());
        }
    }
}
