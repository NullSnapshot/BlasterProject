using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal interface ISpawnerObserver
    {
        public void UpdateObservers(WaveSpawner originator);
    }
}
