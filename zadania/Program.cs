using System.Collections.Generic;

int width = 20;
int height = 20;
int n = 15000;
double[,] generateTable(int x, int y, Func<int, int, double> generator)
{
    double[,] Table = new double[x,y];
    for (int i = 0; i < width; i++)
    {
        for (int j = 0; j < height; j++)
            Table[i,j] = generator(x, y);
    }
    return Table;
}
List<Tuple<double,int,int>> generateHubs(double[,] table, int amount, Random random)
{
    List<Tuple<double, int, int>> hubs = new List<Tuple<double, int, int>>();
    for(int x = 0; x<amount; x++)
    {
        int h = random.Next(height-1);
        int w = random.Next(width-1);
        Tuple<double, int, int> hub = new Tuple<double, int, int>(table[h, w], h, w);
        if (!hubs.Contains(hub))
        hubs.Add(hub);
    }
    return hubs;
}
double[] generateDistribution(List<Tuple<double,int,int>> hubs)
{
    double[] distribution = new double[hubs.Count];
    distribution[0] = hubs[0].Item1;
    for (int i = 1; i < hubs.Count; i++)
        distribution[i]= hubs[i].Item1 + distribution[i-1];
    return distribution;
}
List<Tuple<int,int,int,int>> generateLinks(List<Tuple<double,int,int>> hubsDensity, List<Tuple<double,int,int>> hubsDesirability, double[] distributionDensity, double[] distributionDesirability, Random ran, int n)
{
    List<Tuple<int, int, int, int>> links = new List<Tuple<int, int, int, int>>();
    for (int i = 0; i < n; i++)
    {
        Tuple<int,int> linksA = generatePoint(hubsDensity, distributionDensity, ran);
        Tuple<int,int> linksB = generatePoint(hubsDesirability, distributionDesirability, ran);
        if (!(linksA.Item1 == linksB.Item1 && linksA.Item2 == linksB.Item2))
            links.Add(new Tuple<int, int, int, int>(linksA.Item1, linksA.Item2, linksB.Item1, linksB.Item2));
    }
    return links;
}
Tuple<int,int> generatePoint(List<Tuple<double, int, int>> hubs, double[] distribution, Random ran)
{
    double maxDistro = generateMaxDistro(distribution);
    double numberToSearchFor = Math.Round(ran.NextDouble()*maxDistro,3);
    int index = binarySearch(distribution, 0, distribution.Length - 1, numberToSearchFor);
    return new Tuple<int, int>(hubs[index].Item2, hubs[index].Item3);
}
double generateMaxDistro(double[] distribution)
{
    double distro = 0;
    for (int i = 0; i<distribution.Length; i++)
    {
        if (distribution[i] > distro)  distro = distribution[i]; 
    }
    return distro;
}
int binarySearch(double[] distribution, int left, int right, double numberToSearchFor)
{
    int mid = 0;
    while(left<right) { 
        mid = (left + right)/2;
        if (distribution[mid] > numberToSearchFor)
            right = mid;
        else
        {
            if (distribution[mid + 1] > numberToSearchFor)
                return mid;
            else
                left = mid;
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
            summary[travel]++;
        else
            summary[travel] = 1;
    }
    foreach (KeyValuePair<string,int> kvp in summary)
        Console.WriteLine($"Podróż {kvp.Key} odbyła się {kvp.Value} razy");
}
void maxTo(List<Tuple<int,int,int,int>> links)
{
    var amountPerHub = links.GroupBy(link => new Tuple<int, int>(link.Item3, link.Item4)).Select(c => new { Key = c.Key, total = c.Count() });
    var result = amountPerHub.MaxBy(x => x.total);
    log($"Najwięcej osób - {result.total} - podróżowało do {result.Key.Item1},{result.Key.Item2}", false);
}
void maxFrom(List<Tuple<int,int,int,int>> links)
{
    var amountPerHub = links.GroupBy(link => new Tuple<int, int>(link.Item1, link.Item2)).Select(c => new { Key = c.Key, total = c.Count() });
    var result = amountPerHub.MaxBy(x => x.total);
    log($"Najwięcej osób - {result.total} - podróżowało z {result.Key.Item1},{result.Key.Item2}", true);
}

void log(string message, bool colorMode)
{
    if (colorMode == false) Console.ForegroundColor = ConsoleColor.Red;
    else Console.ForegroundColor=ConsoleColor.Green;
    Console.WriteLine(message);
    Console.ForegroundColor = ConsoleColor.White;
}

double[,] densityTable = generateTable(height, width, (x,y)=> ((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / (8.0)) + 4);
double[,] desirabilityTable = generateTable(height, width, (x,y)=> ((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / (8.0)) + 4);
Random rand = new Random();
int hubsAmount = rand.Next(0, (int)(5 * Math.Sqrt(height * width))); //ilość hubów do wygenerowania, ograniczona, żeby nie było ich miliard na mapie 10*10
List<Tuple<double, int, int>> hubsDensity = generateHubs(densityTable, hubsAmount, rand);
List<Tuple<double, int, int>> hubsDesirability = generateHubs(desirabilityTable, hubsAmount, rand);
double[] distributionDensity = generateDistribution(hubsDensity);
double[] distributionDesirability = generateDistribution(hubsDesirability);
List<Tuple<int,int,int,int>> links = generateLinks(hubsDensity, hubsDesirability, distributionDensity, distributionDesirability, rand, n);
maxTo(links);
maxFrom(links);
summary(links);
