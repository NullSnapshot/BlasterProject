using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal interface ISpawnerSubject
    {
        public void Attach(ISpawnerObserver observer);
        public void Detach(ISpawnerObserver observer);
    }
}
