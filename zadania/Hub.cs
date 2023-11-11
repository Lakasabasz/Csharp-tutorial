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
        double value;
        public Hub(double value, int x, int y) { coordinates = new Tuple<int, int>(x, y); this.value = value; }
        public void setProbability(double probability)
        {
            this.probability = probability;
        }
        public double getValue()
        {
            return value;
        }
        public void addPipe(Pipe pipe)
        {
            this.pipes.Add(pipe);
        }
        public Tuple<int, int> getCoordinates()
        {
            return coordinates;
        }
    }
}
