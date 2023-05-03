using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class MobEntity : Entity
    {
        public int health { get; set; }
        public bool alive { get; set; } = true;
        
        public MobEntity(EntityBehavior behavior, Texture2D texture, Vector2 position, int health)
            : base(behavior, texture, position)
        {
            this.health = health;
        }

    }
}
