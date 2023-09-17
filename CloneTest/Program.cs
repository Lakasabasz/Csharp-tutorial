// Funkcje
// [akcesor] <typ zwracany> <nazwa>([argument,])

// Funkcja jednoliniowa
void Log(string log) => Console.WriteLine(DateTime.Now.ToString("s") + $"\t{log}");

// Funkcja wieloliniowa
void LogType(int level, string log)
{
    string levelName = level switch
    {
        0 => "Debug",
        1 => "Info",
        2 => "Warn",
        3 => "Error",
        _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
    };
    Console.WriteLine($"[{levelName}] " + DateTime.Now.ToString("s") + $"\t{log}");
}

// Funkcja z parametrem domyślnym
void LogExtended(string log, object? extra = null)
{
    Console.WriteLine(DateTime.Now.ToString("s") + $"\t{log}\n{extra?.ToString() ?? string.Empty}");
}
// LogExtended("Log bez domyślnego parametru");


// Funkcje anonimowe|lambda
Action lambda = () => Console.WriteLine("Empty"); // Funkcja nie zwraca wartości i nie przyjmuje żadnego
Action<int> paramLambda = value => Console.WriteLine(value * value); // Funkcja przyjmuje parametr typu int
Func<int> returnLambda = () => new Random().Next(); // Funkcja zwraca losową wartość
Func<int, int, int> returnLambdaParam = (min, max) => new Random().Next(min, max);

// LINQ

var collection = Enumerable.Range(0, 100).ToList(); // Generowanie ciągu kolejnych liczb od 0 do 100
collection.ForEach(x => Console.Write($"{x}, ")); // Brzydki foreach wyświetlający listę liczb 
var modified = collection.Select(x => x*x); // Modyfikacja każdego elementu
var filtered = modified.Where(x => 100 < x && x < 1000); // Filtrowanie elementów które są 100 < x < 1000
var materialized = filtered.ToList(); // Do tego momentu kolekcję można było przeglądnąć tylko raz, potem znikała.
                                      // Teraz będzie jako zwykła lista