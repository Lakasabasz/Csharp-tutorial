using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadania
{
    internal class Map
    {
        double[,] data;
        //double[,] distribution;
        List<Hub> hubs;
        public Map(int x, int y, bool mode, Func<int, int, int, int, double> generator)
        {
            int a = 0, b = 0;
            if (mode == true) { a = x; b = y; }
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    data[i, j] = generator(i, j, a, b);
                }
            }
        }
        private void addHub (Hub hub)
        {
           hubs.Add(hub);
        }
        private Hub GetRandomHub()
        {
            return hubs[0];
            //cdn
        }
        private Hub GetProbableHub()
        {
            return hubs[0];
            //cdn
        }

    }
}
