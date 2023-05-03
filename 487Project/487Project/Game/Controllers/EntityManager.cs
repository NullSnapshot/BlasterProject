using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal static class EntityManager
    {
        private static List<Entity> entities = new List<Entity>();
        private static List<Entity> collisionEntities = new List<Entity>();
        private static Entity User { get; set; }

        public static void RegisterUser(Entity user)
        {
            entities.Add(user);
            collisionEntities.Add(user);
            User = user;
        }

        public static void RegisterCollidableEntity(Entity collidable)
        {
            entities.Add(collidable);
            collisionEntities.Add(collidable);
        }

        public static void Update(GameTime gameTime)
        {
            // Update positional/firing logic
            foreach (Entity entity in entities)
            {
                entity.Update(gameTime);
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Entity entity in entities)
            {
                entity.Draw(spriteBatch, gameTime);
            }
        }
        public static void DeregisterCollidableEntity(Entity collidable) 
        { 
            entities.Remove(collidable);
            collisionEntities.Remove(collidable);
        }
    }
}
