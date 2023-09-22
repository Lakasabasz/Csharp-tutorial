int width = 100;
int height = 100;
double[,] generateTableDensity(int width, int height)
{
    double[,] densityTable = new double[width, height];
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            densityTable[x, y] = -((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / (8)) + 4;
        }
    }
    return densityTable;
}
double[,] generateTableDesirability(int width, int height)
{
    double[,] desirabilityTable = new double[width, height];
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            desirabilityTable[x, y] = -Math.Abs((x - width / 2.0) * (y - height / 2.0)) + (width / 2.0 * height / 2.0);
        }
    }
    return desirabilityTable;
}
List<Tuple<double,int,int>> generateHubsDensity(double[,] densityTable, int masterRandomNumber, Random random)
{
    List<Tuple<double, int, int>> hubs = new List<Tuple<double, int, int>>();
    for(int x = 0; x<masterRandomNumber; x++)
    {
        int h = random.Next(height-1);
        int w = random.Next(width-1);
        hubs.Add( new Tuple<double, int, int>(densityTable[h, w], h, w));
    }
    return hubs;
}
List<Tuple<double,int,int>> generateHubsDesirability(double[,] desirabilityTable, int masterRandomNumber, Random random)
{
    List<Tuple<double, int, int>> hubs = new List<Tuple<double, int, int>>();
    for (int x = 0; x<masterRandomNumber; x++)
    {
        int h = random.Next(height-1);
        int w = random.Next(width-1);
        hubs.Add(new Tuple<double, int, int>(desirabilityTable[h, w], h, w));
    }
    return hubs;
}
double[] generateDistributionDensity(List<Tuple<double,int,int>> hubs, int masterRandomNumber)
{
    double[] distribution = new double[masterRandomNumber];
    distribution[0] = hubs[0].Item1;
    for (int i = 1; i < masterRandomNumber; i++)
    {
        distribution[i]= hubs[i].Item1 + distribution[i-1];
    }
    return distribution;
}
double[] generateDistributionDesirability(List<Tuple<double,int,int>> hubs, int masterRandomNumber)
{
    double[] distribution = new double[masterRandomNumber];
    distribution[0] = hubs[0].Item1;
    for (int i = 1; i < masterRandomNumber; i++)
    {
        distribution[i]= hubs[i].Item1 + distribution[i-1];
    }
    return distribution;
}




double[,] densityTable = generateTableDensity(width, height);
double[,] desirabilityTable = generateTableDesirability(width, height);
Random rand = new Random();
int masterRandomNumber = rand.Next(0, Int32.Parse((5 * Math.Sqrt(height * width)).ToString()));
List<Tuple<double, int, int>> hubsDensity = generateHubsDensity(densityTable, masterRandomNumber, rand);
List<Tuple<double, int, int>> hubsDesirability = generateHubsDesirability(desirabilityTable, masterRandomNumber, rand);
double[] distributionDensity = generateDistributionDensity(hubsDensity, masterRandomNumber);
double[] distributionDesirability = generateDistributionDesirability(hubsDesirability, masterRandomNumber);
//randomową liczbe z zakresu 0 do max(dystrybuanta)
//wyszukiwaniem przez podział - coś jak quicksort troche chyba - wyszukac indeks, taki, ze dystr[i] < nasz x < dystr[i+1]
//odkopać wtedy po dystrybuancie kordy tego
//zapisać kordy z obu map do listy przez tuple (w sensie pierwszy z pierwszym, drugi z drugim, etc.)
//potem wypluć na cmd podsumowanie - jako, że linki mogą występować więcej niż raz - od najbardziej popularnych do najmniej
//usunąć linki w to samo miejsce - że w obu komórkach tupla beda te same wartosci
//to chyba wsio