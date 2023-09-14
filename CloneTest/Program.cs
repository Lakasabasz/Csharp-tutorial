// See https://aka.ms/new-console-template for more information

/*
string? text = null;
text ??= "Jeśli widzisz ten tekst to znaczy, że wszystko działa poprawnie";
Console.WriteLine(text);
*/


namespace Zadanie1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Podaj wymiar mapy gestosci, skladowa X");
            int densitySizeX = 7; // will be repaced with actual variable in future

            // Console.WriteLine("Podaj wymiar mapy gestosci, skladowa Y");
            int densitySizeY = 10; // will be repaced with actual variable in future

            var densityMap = GenerateDensityMap(densitySizeX, densitySizeY);
            var desirabilityMap = GenerateDesirabilityMap(densitySizeX, densitySizeY);

            // Console.WriteLine("Podaj liczbę węzłów");
            int noOfHubs = 30; // will be repaced with actual variable in future

            // Console.WriteLine("Podaj liczbę punktów powiązania (N)");
            int noOfLinks = 5; // will be repaced with actual variable in future

            // hubs indexes
            var hubs = new List<Tuple<int, int>>();

            // density & desirability - index + value
            var hubsDesity = new List<Tuple<int, int, double>>();
            var hubsDesirability = new List<Tuple<int, int, double>>();


            Random rnd = new Random();
            for (int i = 0; i < noOfHubs; ++i)
            {
                var x = rnd.Next(0, densitySizeX);
                var y = rnd.Next(0, densitySizeY);
                hubs.Add(Tuple.Create(x, y));
                hubsDesity.Add(Tuple.Create(x, y, densityMap[x, y]));
                hubsDesirability.Add(Tuple.Create(x, y, desirabilityMap[x, y]));

            }

            var densityDistribution = PrepareHubDistribution(hubsDesity);
            var desirabilityDistribution = PrepareHubDistribution(hubsDesirability);

            //Console.WriteLine("Density map");
            //Print2DMap(densityMap, densitySizeX, densitySizeY);

            //Console.WriteLine("Desirability map");
            //Print2DMap(desirabilityMap, densitySizeX, densitySizeY);

            var densityDistMax = densityDistribution.Max();
            var desirabilityDistyMax = desirabilityDistribution.Max();

            var densityPoints = new List<int>();
            var desirabilityPoints = new List<int>();

            for (int pointNo = 0; pointNo < noOfLinks; ++pointNo)
            {
                var densityVal = rnd.Next(0, (int)densityDistMax + 1);
                var desirabilityVal = rnd.Next(0, (int)desirabilityDistyMax + 1);

                var densityId = BinarySearch(densityDistribution, densityVal);
                var desirabilityId = BinarySearch(desirabilityDistribution, desirabilityVal);

                if (hubsDesity[densityId].Item1 == hubsDesirability[desirabilityId].Item1 &&
                    hubsDesity[densityId].Item2 == hubsDesirability[desirabilityId].Item2)
                {
                    --pointNo;
                    continue;
                }
                densityPoints.Add(densityId);
                desirabilityPoints.Add(desirabilityId);
            }

            for (int i = 0; i < densityPoints.Count(); ++i)
            {
                int densityId = densityPoints[i];
                int desirabilityId = desirabilityPoints[i];
                Console.WriteLine("Travel from x: " + hubsDesity[densityId].Item1 +
                    " y: " + hubsDesity[densityId].Item2 +
                    " density: " + hubsDesity[densityId].Item3 +
                    ", to x: " + hubsDesirability[desirabilityId].Item1 +
                    " y: " + hubsDesirability[desirabilityId].Item2 +
                    " desirability: " + hubsDesirability[desirabilityId].Item3);
            }

        }


        static int ReadInt()
        {
            string? linia = Console.ReadLine();
            _ = linia ?? throw new ArgumentException("Oczekiwano wartosci typu int");
            int liniaInt;
            bool success = int.TryParse(linia, out liniaInt);
            if (!success) { throw new ArgumentException("Nie udało się przetworzyć " + linia + " na int"); }

            return liniaInt;
        }


        static double[,] GenerateDensityMap(int densitySizeX, int densitySizeY)
        {
            double[,] densityMap = new double[densitySizeX, densitySizeY];

            Func<int, int, double> element = (x, y) => (-((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / (8)) + 4);
            double minCandidate = element(0, 0);

            for (int x = 0; x < densitySizeX; ++x)
            {
                for (int y = 0; y < densitySizeY; ++y)
                {
                    densityMap[x, y] = element(x, y);

                    minCandidate = Math.Min(minCandidate, densityMap[x, y]);
                }
            }

            if (minCandidate < 0)
            {
                for (int x = 0; x < densitySizeX; ++x)
                {
                    for (int y = 0; y < densitySizeY; ++y)
                    {
                        densityMap[x, y] -= minCandidate;
                    }
                }

            }

            return densityMap;
        }

        static double[,] GenerateDesirabilityMap(int desirabilitySizeX, int desirabilitySizeY)
        {
            double[,] desirabilityMap = new double[desirabilitySizeX, desirabilitySizeY];

            for (int x = 0; x < desirabilitySizeX; ++x)
            {
                for (int y = 0; y < desirabilitySizeY; ++y)
                {
                    desirabilityMap[x, y] = -Math.Abs((x - desirabilitySizeX / 2.0) * (y - desirabilitySizeY / 2.0)) + (desirabilitySizeX / 2.0 * desirabilitySizeY / 2.0);
                }
            }

            return desirabilityMap;
        }

        static void Print2DMap(double[,] map, int xSize, int ySize)
        {
            for (var i = 0; i < xSize; ++i)
            {
                for (var j = 0; j < ySize; ++j)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }

        }

        static List<Double> PrepareHubDistribution(List<Tuple<int, int, double>> idValMap)
        {
            List<Double> result = new List<double>();
            if (idValMap.Count() == 0)
            {
                return result;
            }
            result.Add(idValMap[0].Item3);

            for (int i = 1; i < idValMap.Count(); ++i)
            {
                result.Add(idValMap[i].Item3 + result[i - 1]);
            }

            return result;
        }

        static int BinarySearch(List<double> tab, double val)
        {
            // ToDo(146karol): do binary search, not just linear
            if (tab.Count() == 0 || val < tab[0])
            {
                return 0;
            }
            for (int i = 0; i < tab.Count() - 1; ++i)
            {
                if (tab[i] < val && val < tab[i + 1])
                {
                    return i;
                }
            }
            return tab.Count();
        }
    }
}