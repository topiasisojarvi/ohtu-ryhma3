using Xunit;
using System;
using System.IO;
using System.Linq;

namespace ReferenceManager.Tests;

[Collection("Konsolitestit")]
public class LisaaTestit
{
    [Fact]
    public void LisaaLahde_OlemassaOlevaTiedosto_KayttaaSeuraavaaIdta()
    {
        var (rivit, _) = AjaLisaa(
            new[]
            {
                "1|kirja|Clean Code|Robert C. Martin|2008",
                "2|artikkeli|No Silver Bullet|Fred Brooks|1986"
            },
            "kirja", "The Pragmatic Programmer", "Andrew Hunt", "1999");

        Assert.Equal(3, rivit.Length);
        Assert.Equal("3|kirja|The Pragmatic Programmer|Andrew Hunt|1999", rivit[2]);
    }

    [Fact]
    public void LisaaLahde_SailyttaaAiemmatRivit()
    {
        var alkuperaiset = new[]
        {
            "1|kirja|Clean Code|Robert C. Martin|2008",
            "2|artikkeli|No Silver Bullet|Fred Brooks|1986"
        };

        var (rivit, _) = AjaLisaa(alkuperaiset, "kirja", "Uusi", "Tekijä", "2020");

        Assert.Equal(alkuperaiset[0], rivit[0]);
        Assert.Equal(alkuperaiset[1], rivit[1]);
    }
    [Fact]
    public void LisaaLahde_TyhjaTiedosto_AloittaaIdsta1()
    {
        var (rivit, _) = AjaLisaa(
            Array.Empty<string>(),
            "kirja", "Uusi", "Tekijä", "2020");

        Assert.Single(rivit);
        Assert.StartsWith("1|", rivit[0]);
    }

    [Fact]
    public void LisaaLahde_IdtEivatPeräkkäisiä_KayttaaSuurinta_Plus1()
    {
        var (rivit, _) = AjaLisaa(
            new[]
            {
                "1|kirja|A|B|2000",
                "7|kirja|C|D|2001",
                "3|kirja|E|F|2002"
            },
            "kirja", "Uusi", "Tekijä", "2020");

        Assert.StartsWith("8|", rivit.Last());
    }

    [Fact]
    public void LisaaLahde_TulostaaUudenIdn()
    {
        var (_, tuloste) = AjaLisaa(
            new[] { "1|kirja|A|B|2000" },
            "kirja", "Uusi", "Tekijä", "2020");

        Assert.Contains("ID: 2", tuloste);
    }

    [Fact]
    public void LisaaLahde_KysyyKaikkiKentat()
    {
        var (_, tuloste) = AjaLisaa(null, "kirja", "Nimi", "Tekijä", "2020");

        Assert.Contains("Tyyppi:", tuloste);
        Assert.Contains("Nimi:", tuloste);
        Assert.Contains("Tekijä:", tuloste);
        Assert.Contains("Vuosiluku:", tuloste);
    }

    [Fact]
    public void LisaaLahde_KaksiLisaysta_SaavatEriIdt()
    {
        string kansio = LuoKansio();
        string tiedosto = Path.Combine(kansio, "lahteet.txt");

        var vanhaSyote = Console.In;
        var vanhaTulostus = Console.Out;
        try
        {
            Console.SetOut(new StringWriter());

            Console.SetIn(new StringReader("kirja\nEnsimmäinen\nA\n2000\n"));
            Lisaa.LisaaLahde(tiedosto);

            Console.SetIn(new StringReader("kirja\nToinen\nB\n2001\n"));
            Lisaa.LisaaLahde(tiedosto);

            var rivit = File.ReadAllLines(tiedosto);
            Assert.Equal(2, rivit.Length);
            Assert.StartsWith("1|", rivit[0]);
            Assert.StartsWith("2|", rivit[1]);
        }
        finally
        {
            Console.SetIn(vanhaSyote);
            Console.SetOut(vanhaTulostus);
            Directory.Delete(Directory.GetParent(kansio)!.FullName, true);
        }
    }

    private static string LuoKansio()
    {
        string juuri = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string data = Path.Combine(juuri, "data");
        Directory.CreateDirectory(data);
        return data;
    }

    // Aja Lisaa.LisaaLahde ja palauta lopuksi tiedoston rivit ja tuloste
    private static (string[] rivit, string tuloste) AjaLisaa(string[]? alkuRivit, params string[] syotteet)
    {
        string kansio = LuoKansio();
        string tiedosto = Path.Combine(kansio, "lahteet.txt");

        if (alkuRivit != null)
            File.WriteAllLines(tiedosto, alkuRivit);

        TextReader vanhaSyote = Console.In;
        TextWriter vanhaTulostus = Console.Out;
        var tulostus = new StringWriter();

        try
        {
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, syotteet)));
            Console.SetOut(tulostus);

            Lisaa.LisaaLahde(tiedosto);

            return (File.ReadAllLines(tiedosto), tulostus.ToString());
        }
        finally
        {
            Console.SetIn(vanhaSyote);
            Console.SetOut(vanhaTulostus);
            Directory.Delete(Directory.GetParent(kansio)!.FullName, true);
        }
    }
}