using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadania
{
    internal class Hub
    {
        Tuple<int, int> coordinates;
        List<Pipe> pipes;
        double probability;
        public Hub(int x, int y) { coordinates = new Tuple<int, int>(x, y); }
    }
}
