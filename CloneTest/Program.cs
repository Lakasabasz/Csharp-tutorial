{
    // Inicjowanie tablicy double 2D
    int x = 5;
    int y = 10;
    double[][] table = new double[x][];
    for (var i = 0; i < table.Length; i++) table[i] = new double[y];
}
{
    // Algorytm wyszukiwania przez podział w wersji iteracyjnej
    int[] numbers = Enumerable.Range(0, 100).Select(x => x*2).ToArray(); // Inicjowanie tablicy wartościami od 0 do 198
    int borderA = 0;
    int borderB = numbers[^1];
    double x = 69.69; // Szukana liczba
    while (borderA + 1 < borderB)
    {
        int center = (borderA + borderB) / 2;
        if (center < x) borderA = center;
        else borderB = center;
    }

    int wynik = borderA;
}