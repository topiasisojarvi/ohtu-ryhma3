namespace ReferenceManager.Tests;
using Xunit;
using System.IO;

[Collection("Konsolitestit")]
public class UnitTest1
{
    [Fact]
    public void Suorita_PoistaaValitunLahteen()
    {
        //luodaan testikansio ja mallilähteet
        string testikansio = Path.Combine(Path.GetTempPath(),Guid.NewGuid().ToString());

        string dataKansio = Path.Combine(testikansio, "data");
        string srcKansio = Path.Combine(testikansio, "src");

        Directory.CreateDirectory(dataKansio);
        Directory.CreateDirectory(srcKansio);

        string tiedosto = Path.Combine(dataKansio, "lahteet.txt");

        File.WriteAllLines(tiedosto, new[]{
            "1|kirja|Clean Code|Robert C. Martin|2008",
            "2|artikkeli|No Silver Bullet|Fred Brooks|1986"
        });

        string vanhaHakemisto = Environment.CurrentDirectory;
        TextReader vanhaSyote = Console.In;

        try
        {
            Environment.CurrentDirectory = srcKansio;

            //poistettava
            Console.SetIn(new StringReader("1"));

            Poista.Suorita();

            string[] rivit = File.ReadAllLines(tiedosto);

            Assert.DoesNotContain(rivit,
                rivi => rivi.StartsWith("1|"));

            Assert.Contains(rivit,
                rivi => rivi.StartsWith("2|"));
        }
        finally
        {
            Console.SetIn(vanhaSyote);
            Environment.CurrentDirectory = vanhaHakemisto;
            Directory.Delete(testikansio, true);
        }
    }
}
