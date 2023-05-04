using Microsoft.Xna.Framework;
using System;

namespace BulletBlaster.Game.Controllers
{
    internal static class ScoreSystem
    {
        public static int Score { get; private set; } = 0;

        public static Vector2 InputPosition { get; set; }

        public static void addPoints(int points)
        {
            Score += points;
        }

        public static void DeductPoints(int points)
        {
            Score = Math.Max(Score - points, 0);
        }

        public static void clearScore()
        {
            Score = 0;
        }

    }
}
