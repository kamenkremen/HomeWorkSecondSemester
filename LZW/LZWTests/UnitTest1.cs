namespace LZWTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("../../../TestFiles/FairyTale.txt", false)]
    [TestCase("../../../TestFiles/FairyTale.txt", true)]
    [TestCase("../../../TestFiles/vlastelin_kolec_bratstvo_kol_ca.txt", false)]
    [TestCase("../../../TestFiles/vlastelin_kolec_bratstvo_kol_ca.txt", true)]
    [TestCase("../../../TestFiles/frog.avif", false)]
    [TestCase("../../../TestFiles/frog.avif", true)]
    [TestCase("../../../TestFiles/Small.txt", true)]
    [TestCase("../../../TestFiles/Small.txt", false)]
    public void TestLZW(string filePath, bool BWT)
    {
        var expected = File.ReadAllText(filePath);
        LZW.Encode(filePath, BWT);
        LZW.Decode(filePath + ".zipped", BWT);
        if (!File.ReadAllText(filePath).Equals(expected))
        {
            Assert.Fail();
        }
    }

    [TestCase("../../../TestFiles/EmptyFile.txt", false)]
    [TestCase("../../../TestFiles/EmptyFile.txt", true)]
    public void TestEncodeEmptyFile(string filePath, bool BWT)
    {
        Assert.Throws<ArgumentException>(() => LZW.Encode(filePath, BWT));
    }

    [TestCase("../../../TestFiles/EmptyFile.txt", false)]
    [TestCase("../../../TestFiles/EmptyFile.txt", true)]
    public void TestDecodeEmptyFile(string filePath, bool BWT)
    {
        Assert.Throws<ArgumentException>(() => LZW.Decode(filePath, BWT));
    }


    [TestCase("amogus", false)]
    [TestCase("amogus", true)]
    public void TestEncodeNotExistingFile(string filePath, bool BWT)
    {
        Assert.Throws<ArgumentException>(() => LZW.Encode(filePath, BWT));
    }

    [TestCase("amogus", false)]
    [TestCase("amogus", true)]
    public void TestDecodeNotExistingFile(string filePath, bool BWT)
    {
        Assert.Throws<ArgumentException>(() => LZW.Decode(filePath, BWT));
    }
}
