using System.Collections.Generic;
using zadania;

int width = 20;
int height = 20;
int n = 15000;
void generatePipes(Map density, Map desirability, Random ran, int amount)
{
    for (int i = 0; i < amount; i++)
    {
        Hub hubA = density.generatePoint(ran);
        Hub hubB = desirability.generatePoint(ran);
        if (!(hubA.getCoordinates().Item1 == hubB.getCoordinates().Item1 && hubA.getCoordinates().Item2 == hubB.getCoordinates().Item2))
        {
            Pipe pipe = new Pipe(hubA, hubB);
        }
    }
}
/* jako że każdy Hub ma swoją listę Pipe'ów, trzeba pomyśleć jak to względnie wydajnie zaimplementować
void summary(List<Tuple<int,int,int,int>> links)
{
    Dictionary<Tuple<int,int,int,int>, int> summary = new Dictionary<Tuple<int,int,int,int>, int>();
    for (int i = 0; i < links.Count; i++)
    {
        Tuple<int,int,int,int> travel = new Tuple<int,int,int,int>(links[i].Item1,links[i].Item2,links[i].Item3,links[i].Item4);
        if (summary.ContainsKey(travel))
            summary[travel]++;
        else
            summary[travel] = 1;
    }
    foreach (KeyValuePair<Tuple<int,int,int,int>,int> kvp in summary)
        Console.WriteLine($"Podróż z {kvp.Key.Item1},{kvp.Key.Item2} do {kvp.Key.Item3},{kvp.Key.Item4} odbyła się {kvp.Value} razy");
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
*/
Map densityMap = new Map(height, width, false, (x,y,a,b)=> ((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / (8.0)) + 4);
Map desirabilityMap = new Map(height, width, true, (x,y,a,b)=> -Math.Abs((x - a / 2.0) * (y - b / 2.0)) + (a / 2.0 * b / 2.0));

Random rand = new Random();
int hubsAmount = rand.Next(0, (int)(5 * Math.Sqrt(height * width))); //ilość hubów do wygenerowania, ograniczona, żeby nie było ich miliard na mapie 10*10


densityMap.generateHubs(hubsAmount, rand, width, height);
desirabilityMap.generateHubs(hubsAmount, rand, width, height);
generatePipes(densityMap, desirabilityMap, rand, n);
//maxTo(links);
//maxFrom(links);
//summary(links);
