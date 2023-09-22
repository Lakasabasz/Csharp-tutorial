using System.Collections.Generic;

int width = 100;
int height = 100;
int n = 5000;
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
List<Tuple<int,int,int,int>> generateLinks(List<Tuple<double,int,int>> hubsDensity, List<Tuple<double,int,int>> hubsDesirability, double[] distributionDensity, double[] distributionDesirability, Random ran, int n)
{
    List<Tuple<int, int, int, int>> links = new List<Tuple<int, int, int, int>>();
    for (int i = 0; i < n; i++)
    {
        Tuple<int,int> linksA = generateLink(hubsDensity, distributionDensity, ran);
        Tuple<int,int> linksB = generateLink(hubsDesirability, distributionDesirability, ran);

        links.Add(new Tuple<int,int,int,int> (linksA.Item1, linksA.Item2, linksB.Item1, linksB.Item2));
    }
    return links;
}
Tuple<int,int> generateLink(List<Tuple<double, int, int>> hubs, double[] distribution, Random ran)
{
    double maxDistro = generateMaxDistro(distribution);
    double randomNumber = ran.NextDouble()*maxDistro;
    //wyszukiwanie przez podział w dystrybuancie
    //wyciąganie kordów - mamy dystrybuante, wyszukaliśmy i - który element dystrybuanty to jest, możemy wtedy wyciągnąć kordy z hubs
    return new Tuple<int,int> (hubs[i].Item2, hubs[i].Item3);
}


double generateMaxDistro(double[] distribution)
{
    double distro = 0;
    for (int i = 0; i<distribution.Length; i++)
    {
        if (distribution[i] > distro) { distro = distribution[i]; }
    }
    return distro;
}



double[,] densityTable = generateTableDensity(width, height);
double[,] desirabilityTable = generateTableDesirability(width, height);
Random rand = new Random();
int masterRandomNumber = rand.Next(0, Int32.Parse((5 * Math.Sqrt(height * width)).ToString()));
List<Tuple<double, int, int>> hubsDensity = generateHubsDensity(densityTable, masterRandomNumber, rand);
List<Tuple<double, int, int>> hubsDesirability = generateHubsDesirability(desirabilityTable, masterRandomNumber, rand);
double[] distributionDensity = generateDistributionDensity(hubsDensity, masterRandomNumber);
double[] distributionDesirability = generateDistributionDesirability(hubsDesirability, masterRandomNumber);
List<Tuple<int,int,int,int>> links = generateLinks(hubsDensity, hubsDesirability, distributionDensity, distributionDesirability, rand, n);
//jakaś funkcja trimLinks usuwająca linki w to samo miejsce
//funkcja summary która tworzy Listę tupli (częstość, element listy links), i potem to jakoś wypluwa na konsolę
