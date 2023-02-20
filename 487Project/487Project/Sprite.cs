using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//This is meant to provide an overall sprite class for all entities.
namespace MainProgram;

public class Sprite {    
    protected readonly Texture2D textrue;
    protected readonly Vector2 origin;
    public Vector2 Position {get; set;}
    public int Speed {get; set;}

    public Sprite(Texture2D tex, Vector2 pos) {
        texture = tex;
        Position = pos;
        Speed = 300;
        origin = new(tex.Width/2, tex.Height/2);
    }
    public virtual void Draw() {
        Globals.SpriteBatch.Draw(texture, position, null, Color.White, 0, origin, 1, UserSpriteEffects.None, 1);
    }
}