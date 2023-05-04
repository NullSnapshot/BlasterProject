using BulletBlaster.Game.config;
using BulletBlaster.Game.Entities.Bullet.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities
{
    internal static class EntityTools
    {
        private static IDictionary<string, Type> BulletPatterns = new Dictionary<string, Type>();

        private static int seed = 0;
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
        /// <summary>
        ///     Returns a position based on the deltaTiming of the current frame.
        /// </summary>
        /// <param name="position">The position of the entity</param>
        /// <param name="gameTime">The current GameTime</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Vector2 DeltaMove(Vector2 position, GameTime gameTime, float ?x = 0, float ?y = 0)
        {
            position.X += x.GetValueOrDefault(0) * 2500 * ( (float)gameTime.ElapsedGameTime.TotalSeconds / Config.TargetFPS);
            position.Y += y.GetValueOrDefault(0) * 2500 * ((float)gameTime.ElapsedGameTime.TotalSeconds / Config.TargetFPS);
            return position;
        }

        public static BulletPattern BuildBulletPattern(BulletPatternConfig config, Texture2D sprite)
        {
            return (BulletPattern)Activator.CreateInstance(BulletPatterns[config.pattern], config, sprite);
        }

        public static void GenerateBulletPatternCollection()
        {
            List<Type> patterns = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes().Where(t => t.BaseType == typeof(BulletPattern))).ToList();
            patterns.Add(typeof(BulletPattern));
            
            foreach (Type type in patterns)
            {
                PropertyInfo patternName = type.GetProperty("Pattern");
                if(patternName != null)
                {
                    BulletPattern assemblyInstance = (BulletPattern)Activator.CreateInstance(type);
                    object value = patternName.GetValue(assemblyInstance);

                    if (value is string patternString)
                    {
                        BulletPatterns.Add(new KeyValuePair<string, Type>(patternString, type));
                    }
                }
            }
        }

        // Need a pseudorandom seed so the game is always deterministic.
        public static int GetPseudoRandomSeed()
        {
            int returnedSeed = seed;
            seed++;
            return returnedSeed;
        }

        public static void ResetSeed()
        {
            seed = 0;
        }
    }
}
