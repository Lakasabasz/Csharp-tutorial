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
{
    // Przykład liczenia dystrybuanty
    // Przykład nie jest najbardziej optymalny. Poszukaj sposobu jak to zoptymalizować. Problem znajduje się w pętli
    double[] mapa = Enumerable.Range(0, 50).Select(x => Math.Pow(x / 50.0, 2)).ToArray(); // Wygenerowana mapa
    double[] dystrybuanta = new double[mapa.Length];
    for (int i = 0; i < 50; i++)
    {
        dystrybuanta[i] = mapa.Take(i + 1).Sum();
    }
}
{
    // Przykład wykorzystania obiektu List<int>
    List<int> list = new()
    {
        5, 0
    }; // Lista zainicjowana 2 wartościami przy tworzeniu
    list.Add(1); // Dodawanie kolejnego elementu na koniec listy
    list.Add(2);
    list.Add(3);
    int v = list[0] + list[2]; // v = 6, pobieranie elementu po indeksie
}