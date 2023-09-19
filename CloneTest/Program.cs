using CloneTest;
using System.Collections.Immutable;
using System.Xml.Serialization;
using static CloneTest.BinarySearch;
using static CloneTest.QuickSort;

internal class Program
{
    private static void Main(string[] args)
    {
        int nCoords, nHubs;
        int xDim, yDim;
        double x, y;

        xDim = 10;  // (Dawniej a) Wymiar tablicy w osi x
        yDim = 10;  // (Dawniej b) Wymiar tablicy w osi y

        nCoords = 100;  //Liczba wylosowanych możliwych lokalizacji Hubów
        nHubs = 70; //Liczba hubów początkowych i końcowych


        //Tworzenie list
        double[,] density = new double[xDim, yDim]; 
        double[,] attractive = new double[xDim, yDim]; 

        for (x = 0; x < xDim; x++) {  //Wypełnienie tablicy Gęstości zaludnienia danymi ze wzoru
            for (y = 0; y < yDim; y++) {
                density[(int)x, (int)y] = Math.Abs((((x - 5) * (x - 5)) + ((y - 5) * (y - 5))) * ((((x - 1) * (x - 1)) + ((y - 3) * (y - 3))) / 8)); //TEMP Math.Abs
            }
        }

        for (x = 0; x < xDim; x++) {  //Wypełnienie tablicy Atrakcyjności danymi ze wzoru
            for (y = 0; y < yDim; y++) {
                attractive[(int)x, (int)y] = Math.Abs((x - xDim / 2.0) * (y - yDim / 2.0)) + (xDim / 2.0 * yDim / 2.0);
            }
        }
        
        //Losowanie hubów
        var hubsDensity = ListValueAssign(density, nCoords); 
        var hubsAttractive = ListValueAssign(attractive, nCoords); 

        //Usuwanie połączeń o tym samym punkcie startowym i końcowym
        for (int i = 0; i < hubsDensity.Count; i++)
        {
            if (hubsDensity[i].Item1 == hubsAttractive[i].Item1 && hubsDensity[i].Item2 == hubsAttractive[i].Item2)
            {
                hubsDensity.RemoveAt(i);
                hubsAttractive.RemoveAt(i);
                i--;
            }
        }

        //Liczenie Dystrybuanty
        var cdfDensity = CdfCalculate(hubsDensity);  //lista dystrybuanty Gęstości
        var cdfAttractive = CdfCalculate(hubsAttractive); //Lista dystrybuanty Atrakcyjności 

        //Losowanie Hubów początkowych i końcowych
        var sources = ChooseHubs(cdfDensity, hubsDensity, nHubs);
        var destinations = ChooseHubs(cdfAttractive, hubsAttractive, nHubs);

        var routes = new List<Tuple<int, int, int, int>>();

        for (int i = 0; i < sources.Count && i < destinations.Count; i++) {
            routes.Add(Tuple.Create(sources[i].Item1, sources[i].Item2, destinations[i].Item1, destinations[i].Item2));
        }

        var routesCount = new Dictionary<Tuple<int, int, int, int>, int>();  //Dictionary zawierające ilość danych połączeń

        foreach (Tuple<int, int, int, int> tuple in routes) { //Liczenie ilości powtarzających się połączeń
            if (routesCount.ContainsKey(tuple)) {
                routesCount[tuple]++;
            }
            else {
                routesCount.Add(tuple, 1);
            }
        }

        var routesSorted = routesCount.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        //var listaaa = new Dictionary<Tuple<int, int, int, int>, int>(routesSorted);

        //Sortowanie listy hubow pod względem liczby wystąpień + Wypisanie raportu
        //OutputReport(QuickSort.SortList(startHubsCount, 0, startHubsCount.Count() - 1), "poczatkowe");
        //OutputReport(QuickSort.SortList(endHubsCount, 0, endHubsCount.Count() - 1), "koncowe");


        /*
        Console.WriteLine(" ");
        foreach (Tuple<int, int> tuple in possibleHubs) {
            Console.WriteLine(tuple.Item1 + " " + tuple.Item2);
        }
        */
        OutputReport(routesSorted);



    }


    private static List<Tuple<int, int, double>> ListValueAssign(double[,] list, int n) {
        var hubs = new List<Tuple<int, int, double>>();

        while (hubs.Count != n)
        {
            hubs.AddRange(GenerateValues(list, (n - hubs.Count)));
            hubs = hubs.GroupBy(x => x).Select(d => d.First()).ToList();
        }
        return hubs;


        static List<Tuple<int, int, double>> GenerateValues(double[,] list, int num) {
            var arr = new List<Tuple<int, int, double>>();
            Random rnd = new();

            int xMax = list.GetLength(0);
            int yMax = list.GetLength(1);

            while (arr.Count != num) {
                int xRnd = rnd.Next(0, xMax);
                int yRnd = rnd.Next(0, yMax);
                arr.Add(Tuple.Create(xRnd, yRnd, list[xRnd, yRnd]));
            }
            return arr;
        }
    }


    private static List<double> CdfCalculate(List<Tuple<int, int, double>> arr) { //Obliczenie dystrybuant i zapisanie ich do zwracanej listy (cdfOut)
        double temp = 0;
        var cdfOut = new List<double>();

        for (int i = 0; i < arr.Count; i++) {
            temp += arr[i].Item3;
            cdfOut.Add(temp);
        }
        return cdfOut;

    }

    private static List<Tuple<int, int>> ChooseHubs(List<double> cdf, List<Tuple<int, int, double>> hubs, int nHubs) {
        var list = new List<Tuple<int, int>>();
        
        for (int i = 0; i < nHubs; i++) {
            double cdfTarget = RandomDouble(cdf[0], cdf.Last());  //Losowanie wartości gestosci (na podstawie dystrybuanty)
            int cdfFound = BinarySearch.SearchClosest(cdf, cdfTarget); //Wyszukiwanie hub'a spełniającego warunek cdf[i] < cdfTarget < cdf[i+1]

            list.Add(Tuple.Create(hubs[cdfFound].Item1, hubs[cdfFound].Item2)); // Przepisanie wartości x i y wylosowanego huba do listy zwracanej
        }
        return list;
    }

    private static void OutputReport(Dictionary<Tuple<int, int, int, int>, int> arr) {
        Console.WriteLine("Polaczenia: ");
        foreach (var dict in arr)
        {
            Console.WriteLine($"({dict.Key.Item1}, {dict.Key.Item2}) ->  ({dict.Key.Item3}, {dict.Key.Item4})  Ilosc: {dict.Value}");
        }
    }

    private static double RandomDouble(double min, double max) { 
        Random rnd = new();

        return (rnd.Next((int)Math.Ceiling(min), (int)Math.Floor(max)));
    }

}