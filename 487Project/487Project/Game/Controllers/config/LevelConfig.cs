using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class LevelConfig
    {
        public PlayerConfig player { get; set; }
        public List<WaveConfig> phases { get; set; }
    }
}
