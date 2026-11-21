public class Lisaa{
static string polku = "../../../../data/lahteet.txt";
public static void Suorita()
    {
        LisaaLahde(polku);
    }

public static void LisaaLahde(string polku)
    {
        string id = GetNextId(polku);
        Console.WriteLine($"ID: {id}");

        Console.Write("Tyyppi: ");
        string tyyppi = Console.ReadLine() ?? "";

        Console.Write("Nimi: ");
        string nimi = Console.ReadLine() ?? "";

        Console.Write("Tekijä: ");
        string tekija = Console.ReadLine() ?? "";

        Console.Write("Vuosiluku: ");
        string vuosi = Console.ReadLine() ?? "";

        string line = string.Join("|", id, tyyppi, nimi, tekija, vuosi);

        File.AppendAllText(polku, line + Environment.NewLine);
    }
private static string GetNextId(string path)
{
    if (!File.Exists(path)) return "1";

    int maxId = File.ReadLines(path)
        .Where(l => !l.StartsWith(";") && l.Contains('|'))
        .Select(l => l.Split('|')[0])
        .Select(s => int.TryParse(s, out int n) ? n : 0)
        .DefaultIfEmpty(0)
        .Max();

    return (maxId + 1).ToString();
}


}
