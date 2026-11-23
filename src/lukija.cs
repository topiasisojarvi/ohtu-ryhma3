using System;
using System.IO;

public class Lukija{
const string OletusPolku = "../../../../data/lahteet.txt";
public static void Suorita (string polku = OletusPolku)
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