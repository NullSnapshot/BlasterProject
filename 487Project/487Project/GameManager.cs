using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;

namespace MainProgram;

public class GameManager {
    private readonly List<Bot> _bots = new();
    public GameManager()
    {
        var botTexture = Globals.Content.Load<Texture2D>("orb-blue");

        var ai = new BasicEnemyOne();
        ai.AddWaypoint(new(100,100));
        ai.AddWaypoint(new(400,100));
        ai.AddWaypoint(new(400,400));
        _bots.Add(new (botTexture, new(50,50))
        {
            MoveAI = ai
        });
    }

    public void Update()
    {
        foreach(var bot in _bots)
        {
            bot.Update();
        }
        base.Update(gameTime);
    }
    public void Draw() 
    {
        foreach (var bot in _bots)
        {
            bot.Draw();
        }
        base.Draw(gameTime);
    }
}