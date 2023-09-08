internal class Program
{
    private static void Main(string[] args)
    {
        int nPeople; 
        int xDim, yDim;
        double x, y;
        Random rnd = new Random();

        xDim = 10;  // (Dawniej a) Wymiar tablicy w osi x
        yDim = 10;  // (Dawniej b) Wymiar tablicy w osi y

        nPeople = 10;  //Liczba wylosowanych komórek



        double[,] density = new double[xDim,yDim];  //Utworzenie tablicy gęstości zaludnienia


        for (x = 0; x < xDim; x++) {  //Wypełnienie tablicy Gęstości zaludnienia danymi ze wzoru
            for (y = 0; y < yDim; y++)
            {
                density[(int)x,(int)y] = -(((x - 5) * (x - 5)) + ((y - 5) * (y - 5))) * ((((x - 1) * (x - 1)) + ((y - 3) * (y - 3))) / 8) + 400;  //TEMP +400; Original +4
            }
        }



        var hubsDensity = new List<Tuple<int, int, double>>();  // inicjacja listy zapisujących dane o losowanych hubach początkowych

        for (int i = 0; i < nPeople; i++) {  //Wybór losowych komórek z tablicy gęstości i zapisanie do listy hubsDensity
            int xRnd = rnd.Next(0, xDim);
            int yRnd = rnd.Next(0, yDim);
            hubsDensity.Add(Tuple.Create(xRnd, yRnd, density[xRnd, yRnd]));
        }


        var cdfDensity = new List<double>();  //Inicjacja listy zapisującej dystrybuantę dla wylosowanych wartości gęstości

        double tempDist = 0;  //Zmienna tymczasowa na potrzeby obliczenia dystrybuant
        
        for (int i = 0; i < nPeople; i++) {  //Obliczenie dystrybuant i zapisanie ich do listy distDens
            tempDist += hubsDensity[i].Item3;
            cdfDensity.Add(tempDist);
        }


        


        /*
        foreach (Tuple<int, int, double> tuple in hubsDensity) {
            Console.WriteLine(tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
        }
        */
        /*
        for (int i = 0; i < distDens.Count(); i++)
        {
            Console.WriteLine(distDens[i]);
        }
        */
        /*
        for (int i = 0; i < yDim; i++) {
            for (int j = 0; j < xDim; j++) {
                Console.Write(density[j,i]);
                Console.Write(" ");
            }
            Console.WriteLine("");
        }
        */

    }
}