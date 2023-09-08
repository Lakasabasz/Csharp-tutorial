internal class Program
{
    private static void Main(string[] args)
    {
        int xDim, yDim;
        double x, y;

        xDim = 10;  // (Dawniej a) Wymiar tablicy w osi x
        yDim = 10;  // (Dawniej b) Wymiar tablicy w osi y

        double[,] desity = new double[xDim,yDim];

        for (x = 0; x < xDim; x++) {
            for (y = 0; y < yDim; y++)
            {
                desity[(int)x,(int)y] = -(((x - 5) * (x - 5)) + ((y - 5) * (y - 5))) * ((((x - 1) * (x - 1)) + ((y - 3) * (y - 3))) / 8) + 4;
            }
        }

        for (int i = 0; i < yDim; i++) {
            for (int j = 0; j < xDim; j++) {
                Console.Write(desity[j,i]);
                Console.Write(" ");
            }
            Console.WriteLine("");
        }

    }
}