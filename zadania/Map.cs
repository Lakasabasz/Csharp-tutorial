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
            this.data = new double[x,y];
            int a = 0, b = 0;
            if (mode == true) { a = x; b = y; }
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    this.data[i, j] = generator(i, j, a, b);
                }
            }
        }
        private void addHub (Hub hub)
        {
           hubs.Add(hub);
        }
        public void generateHubs(int amount, Random random, int x, int y)
        {
            this.hubs = new List<Hub>();
            for (int i = 0; i < amount; i++)
            {
                int h = random.Next(x - 1);
                int w = random.Next(y - 1);
                Hub hub = new Hub(this.data[h, w], h, w);
                if (!hubs.Contains(hub))
                    addHub(hub);
            }
            generateDistribution();
        }
        public void generateDistribution()
        {
            double[] distribution = new double[hubs.Count];
            distribution[0] = hubs[0].getValue();
            hubs[0].setProbability(distribution[0]);
            for (int i = 1; i < hubs.Count; i++)
            {
                distribution[i] = hubs[i].getValue() + distribution[i - 1];
                hubs[i].setProbability(distribution[i]);
            }
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
