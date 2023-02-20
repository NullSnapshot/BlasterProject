using Microsoft.Xna.Framework;

//This is here to just handle AI movement.  
//Is a nice helper class nothing more
namespace MainProgram;

public abstract class MovementAI
{
    public abstract void Move(Sprite bot);
}