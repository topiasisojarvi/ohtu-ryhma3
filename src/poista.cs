using System;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Collections.Generic;

public class Poista{
static string polku = "../data/lahteet.txt";
public static void Suorita ()
{
    Console.WriteLine("Valitse poistettava lähde: ");

    List<string> rivit = new List<string>();

    foreach(string rivi in File.ReadLines(polku))
        {
            if (!rivi.TrimStart().StartsWith(";"))
            {
                Console.WriteLine(rivi);
            }
        }

        string poistettavanId = Console.ReadLine();

        string[] kaikkiRivit = File.ReadAllLines(polku);

        string[] uudetRivit = kaikkiRivit
            .Where(rivi => !rivi.TrimStart().StartsWith(poistettavanId))
            .ToArray();

            File.WriteAllLines(polku, uudetRivit);

            Console.WriteLine("Lähde poistettu");
} 

          
}