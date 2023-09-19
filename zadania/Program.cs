//string[] json;
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

/*try
{
    json = File.ReadAllLines("tables.json");

}
catch (FileNotFoundException e)
{*/
double[,] densityTable = generateTableDensity(width, height);
double[,] desirabilityTable = generateTableDesirability(width, height);
//}
Random rand = new Random();
//wybór randomowej liczby randomowych punktów z obu map
//ustawiłbym górny zakres randa np. jako 2*sqrt(x*y), co by nie było ich zbyt dużo, przynajmniej na razie - potem może będzie wskazane więcej
//dicty z pushowanymi do stringa koordynatami (lub w tuple) i wartością - po jednym dla mapy
//dystrybuanty posumować z tych dictów - to jakieś tablice o rozmiarze naszego randa wciąż
//randomową liczbe z zakresu 0 do max(dystrybuanta)
//wyszukiwaniem przez podział - coś jak quicksort troche chyba - wyszukac indeks, taki, ze dystr[i] < nasz x < dystr[i+1]
//odkopać wtedy po dystrybuancie kordy tego
//zapisać kordy z obu map do listy przez tuple (w sensie pierwszy z pierwszym, drugi z drugim, etc.)
//potem wypluć na cmd podsumowanie - jako, że linki mogą występować więcej niż raz - od najbardziej popularnych do najmniej
//usunąć linki w to samo miejsce - że w obu komórkach tupla beda te same wartosci
//to chyba wsio