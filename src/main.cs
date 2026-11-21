using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Ohjelma käynnistyy!!! ");
        Console.WriteLine("Valitse toiminto: \nEnter = lisää \nP = poista \nL = lue");

        while (true)
        {
            ConsoleKeyInfo painallus = Console.ReadKey(true);

            if (painallus.Key == ConsoleKey.Enter)
            {
                Lisaa.Suorita();
            }

            if (painallus.Key == ConsoleKey.P)
            {
                Poista.Suorita();
            }

            if (painallus.Key == ConsoleKey.L){
                Lukija.Suorita();
            }
        }
    }
}