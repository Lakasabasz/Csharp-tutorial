using System.Collections.Generic;

int width = 20;
int height = 20;
int n = 15000;
double[,] generateTableDensity(int width, int height)
{
    double[,] densityTable = new double[width, height];
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            densityTable[x, y] = ((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / (8.0)) + 4;
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
        if (!(linksA.Item1 == linksB.Item1 && linksA.Item2 == linksB.Item2))
        {
            links.Add(new Tuple<int, int, int, int>(linksA.Item1, linksA.Item2, linksB.Item1, linksB.Item2));
        }
    }
    return links;
}
Tuple<int,int> generateLink(List<Tuple<double, int, int>> hubs, double[] distribution, Random ran)
{
    double maxDistro = generateMaxDistro(distribution);
    double randomNumber = Math.Round(ran.NextDouble()*maxDistro,3);
    int index = binarySearch(distribution, 0, distribution.Length - 1, randomNumber);
    return new Tuple<int, int>(hubs[index].Item2, hubs[index].Item3);
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
int binarySearch(double[] distribution, int left, int right, double numberToSearchFor)
{
    int mid = 0;
    while(left<right) { 
        mid = (left + right)/2;
        if (distribution[mid] > numberToSearchFor)
        {
            right = mid;
        }
        else
        {
            if (distribution[mid + 1] > numberToSearchFor)
            {
                return mid;
            }
            else
            {
                left = mid;
            }
        }
    }
    return mid;
}
void summary(List<Tuple<int,int,int,int>> links)
{
    Dictionary<string, int> summary = new Dictionary<string, int>();
    for (int i = 0; i < links.Count; i++)
    {
        string travel = $"z {links[i].Item1},{links[i].Item2} do {links[i].Item3},{links[i].Item4}";
        if (summary.ContainsKey(travel))
        {
            summary[travel]++;
        }
        else
        {
            summary[travel] = 1;
        }
    }
    foreach (KeyValuePair<string,int> kvp in summary)
    {
        Console.WriteLine("Podróż {0} odbyła się {1} razy", kvp.Key, kvp.Value);
    }
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
summary(links);
//funkcja summary która tworzy Listę tupli (częstość, element listy links), i potem to jakoś wypluwa na konsolę
