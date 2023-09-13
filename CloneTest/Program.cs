// See https://aka.ms/new-console-template for more information

/*
string? text = null;
text ??= "Jeśli widzisz ten tekst to znaczy, że wszystko działa poprawnie";
Console.WriteLine(text);
*/


/*
int? rozmiar_gestosc_x = null;

double[,] gestosci = { { } };

*/

using static System.Net.Mime.MediaTypeNames;

namespace Zadanie1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj wymiar mapy gestosci, skladowa X");
            int density_size_x = ReadInt();

            Console.WriteLine("Podaj wymiar mapy gestosci, skladowa Y");
            int density_size_y = ReadInt();

            var density_map = GenerateDensityMap(density_size_x, density_size_y);
            var desirability_map = GenerateDesirabilityMap(density_size_x, density_size_y);

            Console.WriteLine("Podaj liczbę węzłów");
            int no_of_hubs = ReadInt();

            Console.WriteLine("Podaj liczbę punktów powiązania (N)");
            int no_of_links = ReadInt();

            // hubs indexes
            var hubs = new List<Tuple<int, int>>();

            // density & desirability - index + value
            var hubs_desity = new List<Tuple<int, int, double>>();
            var hubs_desirability = new List<Tuple<int, int, double>>();

            // distributions
            //double[] density_distribution = new double[no_of_hubs];
            //double[] desirability_distribution = new double[no_of_hubs];

            Random rnd = new Random();
            for (int i = 0; i < no_of_hubs; ++i)
            {
                var x = rnd.Next(0, density_size_x);
                var y = rnd.Next(0, density_size_y);
                hubs.Add(Tuple.Create(x, y));
                hubs_desity.Add(Tuple.Create(x, y, density_map[x, y]));
                hubs_desirability.Add(Tuple.Create(x, y, desirability_map[x, y]));

            }

            var density_distribution = PrepareHubDistribution(hubs_desity);
            var desirability_distribution = PrepareHubDistribution(hubs_desirability);

            //Console.WriteLine("Density map");
            //Print2DMap(density_map, density_size_x, density_size_y);

            //Console.WriteLine("Desirability map");
            //Print2DMap(desirability_map, density_size_x, density_size_y);

            var density_dist_max = density_distribution.Max();
            var desirability_dist_max = desirability_distribution.Max();

            var density_points = new List<int>();
            var desirability_points = new List<int>();

            for (int point_no = 0; point_no < no_of_links; ++point_no)
            {
                var density_val = rnd.Next(0, (int)density_dist_max + 1);
                var desirability_val = rnd.Next(0, (int)desirability_dist_max + 1);

                var density_id = BinarySearch(density_distribution, density_val);
                var desirability_id = BinarySearch(desirability_distribution, desirability_val);

                if (hubs_desity[density_id].Item1 == hubs_desirability[desirability_id].Item1 &&
                    hubs_desity[density_id].Item2 == hubs_desirability[desirability_id].Item2)
                {
                    continue;
                }
                density_points.Add(density_id);
                desirability_points.Add(desirability_id);
            }

            for (int i = 0; i < density_points.Count(); ++i)
            {
                int density_id = density_points[i];
                int desirability_id = desirability_points[i];
                Console.WriteLine("Travel from x: " + hubs_desity[density_id].Item1 +
                    " y: " + hubs_desity[density_id].Item2 +
                    " density: " + hubs_desity[density_id].Item3 +
                    ", to x: " + hubs_desirability[desirability_id].Item1 +
                    " y: " + hubs_desirability[desirability_id].Item2 +
                    " desirability: " + hubs_desirability[desirability_id].Item3);
            }

        }

        static int ReadInt()
        {
            string? linia = Console.ReadLine();
            if (linia == null)
            {
                throw new ArgumentException("Oczekiwano wartosci typu int");
            }
            int linia_int = Convert.ToInt32(linia);

            return linia_int;
        }

        static double[,] GenerateDensityMap(int density_size_x, int density_size_y)
        {
            double[,] density_map = new double[density_size_x, density_size_y];

            Func<int, int, double> element = (x, y) => (-((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / (8)) + 4);
            double min_candidate = element(0, 0);

            for (int x = 0; x < density_size_x; ++x)
            {
                for (int y = 0; y < density_size_y; ++y)
                {
                    density_map[x, y] = element(x, y);

                    min_candidate = Math.Min(min_candidate, density_map[x, y]);
                }
            }

            if (min_candidate < 0)
            {
                for (int x = 0; x < density_size_x; ++x)
                {
                    for (int y = 0; y < density_size_y; ++y)
                    {
                        density_map[x, y] -= min_candidate;
                    }
                }

            }

            return density_map;
        }

        static double[,] GenerateDesirabilityMap(int desirability_size_x, int desirability_size_y)
        {
            double[,] desirability_map = new double[desirability_size_x, desirability_size_y];

            for (int x = 0; x < desirability_size_x; ++x)
            {
                for (int y = 0; y < desirability_size_y; ++y)
                {
                    desirability_map[x, y] = -Math.Abs((x - desirability_size_x / 2.0) * (y - desirability_size_y / 2.0)) + (desirability_size_x / 2.0 * desirability_size_y / 2.0);
                }
            }

            return desirability_map;
        }

        static void Print2DMap(double[,] map, int x_size, int y_size)
        {
            for (var i = 0; i < x_size; ++i)
            {
                for (var j = 0; j < y_size; ++j)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.Write("\n");
            }

        }

        static List<Double> PrepareHubDistribution(List<Tuple<int, int, double>> id_val_map)
        {
            List<Double> result = new List<double>();
            if (id_val_map.Count() == 0)
            {
                return result;
            }
            result.Add(id_val_map[0].Item3);

            for (int i = 1; i < id_val_map.Count(); ++i)
            {
                result.Add(id_val_map[i].Item3 + result[i - 1]);
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