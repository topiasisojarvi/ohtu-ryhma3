using Xunit;
using System;
using System.IO;

namespace ReferenceManager.Tests;

[Collection("Konsolitestit")]
public class LukijaTestit
{
    [Fact]
    public void Suorita_TulostaaLahteetIlmanPystyviivoja()
    {
        string tulos = AjaLukija(new[]
        {
            ";Kommenttirivi",
            ";id|tyyppi|nimi|tekijä|vuosiluku",
            "1|kirja|Clean Code|Robert C. Martin|2008",
            "2|artikkeli|No Silver Bullet|Fred Brooks|1986"
        });

        Assert.Contains("1 kirja Clean Code Robert C. Martin 2008", tulos);
        Assert.Contains("2 artikkeli No Silver Bullet Fred Brooks 1986", tulos);
        Assert.DoesNotContain("|", tulos);
    }

    [Fact]
    public void Suorita_TyhjaTiedosto_TulostaaIlmoituksen()
    {
        string tulos = AjaLukija(Array.Empty<string>());

        Assert.Contains("Tiedosto on tyhjä", tulos);
    }

    [Fact]
    public void Suorita_VainKommentteja_TulostaaIlmoituksen()
    {
        string tulos = AjaLukija(new[]
        {
            ";Kommenttirivi",
            ";id|tyyppi|nimi|tekijä|vuosiluku"
        });

        Assert.Contains("Tiedosto on tyhjä", tulos);
    }
    // Luo väliaikaisen tiedoston ja ajaa Lukija.Suorita. Palauttaa lopuksi tulosteen.
    private static string AjaLukija(string[] rivit)
    {
        string testikansio = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string dataKansio = Path.Combine(testikansio, "data");
        Directory.CreateDirectory(dataKansio);

        string tiedosto = Path.Combine(dataKansio, "lahteet.txt");
        File.WriteAllLines(tiedosto, rivit);

        TextWriter vanhaTulostus = Console.Out;
        var tulostus = new StringWriter();

        try
        {
            Console.SetOut(tulostus);
            Lukija.Suorita(tiedosto);
            return tulostus.ToString();
        }
        finally
        {
            Console.SetOut(vanhaTulostus);
            Directory.Delete(testikansio, true);
        }
    }
}