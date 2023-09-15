using CloneTest;
using System.Collections.Immutable;
using System.Xml.Serialization;
using static CloneTest.BinarySearch;
using static CloneTest.Quicksort;

internal class Program
{
    private static void Main(string[] args)
    {
        int nPeople, nHubs; 
        int xDim, yDim;
        double x, y;

        xDim = 10;  // (Dawniej a) Wymiar tablicy w osi x
        yDim = 10;  // (Dawniej b) Wymiar tablicy w osi y

        nPeople = 100;  //Liczba wylosowanych komórek

        nHubs = 50; //Liczba hubów początkowych i końcowych


        //Tworzenie list
        double[,] density = new double[xDim,yDim];  //Utworzenie tablicy gęstości zaludnienia
        double[,] attractive = new double[xDim,yDim];  //Utworzenie tablicy atrakcyjności

        for (x = 0; x < xDim; x++) {  //Wypełnienie tablicy Gęstości zaludnienia danymi ze wzoru
            for (y = 0; y < yDim; y++) {
                density[(int)x,(int)y] = -(((x - 5) * (x - 5)) + ((y - 5) * (y - 5))) * ((((x - 1) * (x - 1)) + ((y - 3) * (y - 3))) / 8) + 400;  //TEMP +400; Original +4
            }
        }

        for (x = 0; x < xDim; x++) {  //Wypełnienie tablicy Atrakcyjności danymi ze wzoru
            for (y = 0; y < yDim; y++) {
                attractive[(int)x, (int)y] = Math.Abs((x - xDim / 2.0) * (y - yDim / 2.0)) + (xDim / 2.0 * yDim / 2.0);
            }
        }

        //Losowanie hubów
        var hubsDensity = ListRandomAssign(density, xDim, yDim, nPeople); //Inicjacja listy zapisującej dane o losowych hubach początkowych wartościami losowymi
        var hubsAttractive = ListRandomAssign(attractive, xDim, yDim, nPeople); //Inicjacja listy zapisującej dane o losowych hubach końcowych wartościami losowymi
        
        //Liczenie Dystrybuanty
        var cdfDensity = CdfCalculate(hubsDensity, nPeople);  //lista dystrybuanty Gęstości
        var cdfAttractive = CdfCalculate(hubsAttractive, nPeople); //Lista dystrybuanty Atrakcyjności 

        //Losowanie Hubów początkowych i końcowych
        var startingHubs = ChooseHubs(cdfDensity, hubsDensity, nHubs);
        var endingHubs = ChooseHubs(cdfAttractive,hubsAttractive,nHubs);


        //Usuwanie Hubów o tym samym punkcie startowym i końcowym
        for (int i = 0; i < startingHubs.Count(); i++) {
            if (startingHubs[i].Item1 == endingHubs[i].Item1 && startingHubs[i].Item2 == endingHubs[i].Item2) {
                startingHubs.RemoveAt(i);
                endingHubs.RemoveAt(i);
                i--;
            }
        }

        //Liczenie hubów o danych koordynatach
        var startHubsCount = CountHubs(startingHubs);
        var endHubsCount = CountHubs(endingHubs);

        //Sortowanie listy hubow pod względem liczby wystąpień + Wypisanie raportu
        OutputReport(Quicksort.SortList(startHubsCount, 0, startHubsCount.Count() - 1), "poczatkowe");
        OutputReport(Quicksort.SortList(endHubsCount, 0, endHubsCount.Count() - 1), "koncowe");

    }


    protected static List<Tuple<int, int, double>> ListRandomAssign(double[,] list, int xMax, int yMax, int nPeople) {
        Random rnd = new();
        var arr = new List<Tuple<int, int, double>>();

        for (int i = 0; i < nPeople; i++) {  //Wybór losowych komórek z tablicy gęstości i zapisanie do listy hubsDensity
            int xRnd = rnd.Next(0, xMax);
            int yRnd = rnd.Next(0, yMax);
            arr.Add(Tuple.Create(xRnd, yRnd, list[xRnd, yRnd]));
        }
        return arr;
    }

    protected static List<double> CdfCalculate(List<Tuple<int, int, double>> arr, int nPeople) { //Obliczenie dystrybuant i zapisanie ich do zwracanej listy (cdfOut)
        double temp = 0;
        var cdfOut = new List<double>();

        for (int i = 0; i < nPeople; i++) {
            temp += arr[i].Item3;
            cdfOut.Add(temp);
        }
        return cdfOut;

    }

    protected static List<Tuple<int, int>> ChooseHubs(List<double> cdf, List<Tuple<int, int, double>> hubs, int nHubs) {
        var list = new List<Tuple<int, int>>();
        
        for (int i = 0; i < nHubs; i++) {
            double cdfTarget = RandomDouble(cdf[0], cdf.Last());  //Losowanie wartości gestosci (na podstawie dystrybuanty)
            int cdfFound = BinarySearch.SearchClosest(cdf, cdfTarget); //Wyszukiwanie hub'a spełniającego warunek cdf[i] < cdfTarget < cdf[i+1]

            list.Add(Tuple.Create(hubs[cdfFound].Item1, hubs[cdfFound].Item2)); // Przepisanie wartości x i y wylosowanego huba do listy zwracanej
        }
        return list;
    }

    protected static List<Tuple<int, int, int>> CountHubs(List<Tuple<int, int>> arr) {
        var outList = new List<Tuple<int, int, int>>();

        while(arr.Count() != 0) {
            var temp = Tuple.Create(arr[0].Item1, arr[0].Item2);
            int count = 0;
            for (int j = 0; j < arr.Count(); j++) {
                if (arr[j].Equals(temp)) {
                    count++;
                    arr.RemoveAt(j);
                    j--;
                }
            }
            outList.Add(Tuple.Create(temp.Item1, temp.Item2, count));
        }

        return outList;
    }

    protected static void OutputReport(List<Tuple<int, int, int>> arr, string str) {
        Console.WriteLine("Huby " + str + ": ");
        for (int i = arr.Count() - 1; i >= 0; i--) {
            Console.WriteLine("X: {0}  Y: {1}  Ilosc: {2}", arr[i].Item1, arr[i].Item2, arr[i].Item3);
        }
    }

    protected static double RandomDouble(double min, double max) { 
        Random rnd = new();

        return min + (max - min) * rnd.NextDouble();
    }
}