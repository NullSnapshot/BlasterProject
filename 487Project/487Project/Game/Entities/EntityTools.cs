using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities
{
    internal static class EntityTools
    {
        public static Rectangle BuildCollisionBox(Entity entity)
        {
            return new Rectangle((int)entity.Position.X - entity.Texture.Width / 2, (int)entity.Position.Y - entity.Texture.Width / 2, entity.Texture.Width, entity.Texture.Height);
        }

        public static Vector2 GetNearestInBoundsLocation(Vector2 targetLocation, Entity entity)
        {
            int entityHeightRadius = entity.Texture.Height / 2;
            int entityWidthRadius = entity.Texture.Width / 2;
            // Right bounds check
            if (targetLocation.X > 1013 - entityWidthRadius)
                targetLocation.X = 1013 - entityWidthRadius;

            // Left bounds check
            if (targetLocation.X < 89 + entityWidthRadius)
                targetLocation.X = 89 + entityWidthRadius;

            // Bottom bounds check
            if (targetLocation.Y > 1266 - entityHeightRadius)
                targetLocation.Y = 1266 - entityHeightRadius;

            // Top bounds check
            if (targetLocation.Y < 104 + entityHeightRadius)
                targetLocation.Y = 104 + entityHeightRadius;

            return targetLocation;

        }

        public static Vector2 DeltaMove(Vector2 position, GameTime gameTime, float ?x = 0, float ?y = 0)
        {
            position.X += x.GetValueOrDefault(0) * 2500 * ( (float)gameTime.ElapsedGameTime.TotalSeconds / Config.TargetFPS);
            position.Y += y.GetValueOrDefault(0) * 2500 * ((float)gameTime.ElapsedGameTime.TotalSeconds / Config.TargetFPS);
            return position;
        }
    }
}
