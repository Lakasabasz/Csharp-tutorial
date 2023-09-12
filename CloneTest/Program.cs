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

            // hubs indexes
            var hubs = new List<Tuple<int, int>>();

            // density & desirability - index + value
            var hubs_desity = new List<Tuple<int, int, double>>();
            var hubs_desirability = new List<Tuple<int, int, double>>();

            // distributions
            double[] density_distribution = new double[no_of_hubs];
            double[] desirabilit_distribution = new double[no_of_hubs];

            Random rnd = new Random();
            for (int i = 0; i < no_of_hubs; ++i)
            {
                var x = rnd.Next(0, density_size_x);
                var y = rnd.Next(0, density_size_y);
                hubs.Add(Tuple.Create(x, y));
                hubs_desity.Add(Tuple.Create(x, y, density_map[x, y]));
                hubs_desirability.Add(Tuple.Create(x, y, desirability_map[x, y]));

                if (i == 0)
                {
                    density_distribution[i] = hubs_desity[i].Item3;
                    desirabilit_distribution[i] = hubs_desirability[i].Item3;
                }
                else
                {
                    density_distribution[i] = hubs_desity[i].Item3 + density_distribution[i - 1];
                    desirabilit_distribution[i] = hubs_desirability[i].Item3 + desirabilit_distribution[i - 1];
                }
            }


            for (int x = 0; x < density_size_x; ++x)
            {
                for (int y = 0; y < density_size_y; ++y)
                {
                    Console.WriteLine("x: " + x + ", y: " + y + " -> " + density_map[x, y]);
                }
            }

            for (int x = 0; x < density_size_x; ++x)
            {
                for (int y = 0; y < density_size_y; ++y)
                {
                    Console.WriteLine("x: " + x + ", y: " + y + " -> " + desirability_map[x, y]);
                }
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

            for (int x = 0; x < density_size_x; ++x)
            {
                for (int y = 0; y < density_size_y; ++y)
                {
                    density_map[x, y] = -((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / (8)) + 4;
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
    }
}