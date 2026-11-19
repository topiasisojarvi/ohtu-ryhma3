using System;
using System.IO;

public class Lukija{
static string polku = "../../../../data/lahteet.txt";
public static void Suorita ()
{
    using StreamReader lukija = new StreamReader(polku);
    for (int i = 0; i < 2; i++){
            lukija.ReadLine();
    }

    string? loput = lukija.ReadToEnd();

    if(!string.IsNullOrWhiteSpace(loput)){
        string korjattu = loput.Replace("|", " ");
        Console.WriteLine(korjattu);
    }
    
    else{
        Console.WriteLine("Tiedosto on tyhjä tai ei sisällä vielä yhtäkään lähdettä");
    }
}      
}