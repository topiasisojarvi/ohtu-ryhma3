using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Ohjelma käynnistyy!!! ");
        Console.WriteLine("Valitse toiminto: p = poista");

        while (true)
        {
            ConsoleKeyInfo painallus = Console.ReadKey(true);

            if (painallus.Key == ConsoleKey.P)
            {
                Poista.Suorita();
            }
        }
    }
}