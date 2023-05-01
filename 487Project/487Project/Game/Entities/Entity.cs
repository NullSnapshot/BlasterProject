using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

/*
 * Currently Unused
 * Needs work
 * 
 * 
 */

namespace MainProgram
{
    public class Entity
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public float speed { get; set; }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                this.texture,
                this.position,
                null,
                Color.White,
                0f,
                new Vector2(this.texture.Width / 2, this.texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
        }

        public void Update(KeyboardState kstate, GameTime gameTime)
        {

        }

    }
}
