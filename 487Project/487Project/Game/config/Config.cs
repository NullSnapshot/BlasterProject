using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.config
{
    // Global configs
    internal static class Config
    {
        internal static int TargetFPS { get; set; } = 60;

        internal static bool DebugMode { get; set; } = false;
    }
}
