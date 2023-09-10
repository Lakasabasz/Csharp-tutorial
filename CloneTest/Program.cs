//szerokosc i wysokosc tablicy
int width = 5;
int height = 8;

//deklaracja tablicy gestosc
double[,] density = new double[width, height];

//deklaracja tablicy atrakcyjnosc
double[,] desirability = new double[width, height];

//int x = 1;
//int y = 1;
int a = 1;
int b = 1;

//deklaracja listy wylosowanych elementow z tablicy gestosci
var hubsDensity = new List<Tuple<int, int, double>>();

//deklaracja listy wylosowanych elementow z tablicy atrakcyjnosci
var hubsDesirability = new List<Tuple<int, int, double>>();

Random rnd = new Random();

//iterujemy sobie przez wszystkie elementy tablicy
for (int x = 0; x < width; x++)
{
    for (int y = 0; y < height; y++)
    {
        //nadajemy wartosci elementowi w tablicy gestosci
        double value = -((x - 5) * (x - 5) + (y - 5) * (y - 5)) * (((x - 1) * (x - 1) + (y - 3) * (y - 3)) / 8.0) + 4; 
        density[x, y] = value;
        //Console.WriteLine(value);
        //i tablicy atrakcyjnosci
        double value_ = -Math.Abs((x - a / 2.0) * (y - b / 2.0)) + (a / 2.0 * b / 2.0);
        desirability[x, y] = value_;
        //Console.WriteLine(value_); 
    }
}

//wybieranie losowych elementow
void draw_elements(int i)
{

    while (i > 0){
        //losowanie do tablicy gestosci
        int rnd_w = rnd.Next(0, width);
        int rnd_h = rnd.Next(0, height);
        hubsDensity.Add(Tuple.Create(rnd_w, rnd_h, density[rnd_w, rnd_h]));
        //losowanie do tablicy atrakcyjnosci
        rnd_w = rnd.Next(0, width);
        rnd_h = rnd.Next(0, height);
        hubsDesirability.Add(Tuple.Create(rnd_w, rnd_h, desirability[rnd_w, rnd_h]));

        i--;
    }
}

draw_elements(5);