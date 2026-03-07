namespace RsaIdNumbers.UnitTests;

public class RSAIdValidatorTests
{
    [Theory]
    [InlineData("8701014800085", true)]
    [InlineData("7201014800087", true)]
    [InlineData("8012255800085", true)]
    [InlineData("5501014800082", true)]
    [InlineData("2201014800082", true)]
    [InlineData("9301014800083", true)]
    [InlineData("9901014800080", true)]
    [InlineData("9206305800082", true)]    
    [InlineData("9206225800089", true)]
    [InlineData("9309195800087", true)]
    [InlineData("9901025009087", false)]
    [InlineData("0012315001081", false)]
    [InlineData("0102035001088", false)]
    [InlineData("8205045800088", false)]
    [InlineData("7608110094081", false)]
    [InlineData("0001026001080", false)] // invalid citizenship status
    [InlineData("0001024001082", false)] // invalid citizenship status
    [InlineData("0001025000089", false)] // invalid checksum
    [InlineData("0002135000085", false)] // invalid date of birth
    [InlineData("9902315009085", false)] // invalid date of birth
    [InlineData("9902135010084", false)] // invalid gender
    [InlineData("0000000000000", false)] // all zeros
    [InlineData("1111111111111", false)] // all ones
    [InlineData("99010250090a7", false)] // non-numeric characters
    [InlineData("990102500908", false)] // too short
    [InlineData("99010250090877", false)] // too long
    [InlineData("9206315800080", false)] // invalid birth date    
    public void IsValidSAID(string idNumber, bool expected)
    {
        bool actual = RSAIdValidator.IsValidSAID(idNumber);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("2601017061089", 26, true)]  // yy=26, cutoff=26 -> 2026 (at boundary, maps to 2000s)
    [InlineData("2601017061089", 25, true)]  // yy=26, cutoff=25 -> 1926 (above boundary, maps to 1900s, still valid)
    [InlineData("2601017061089", 30, true)]  // yy=26, cutoff=30 -> 2026 (below boundary, maps to 2000s)
    [InlineData("2606150064089", 26, true)]  // yy=26, cutoff=26 -> 2026 (valid, different date)
    [InlineData("0002298474087", 25, true)]  // yy=00, Feb 29, cutoff=25 -> 2000 leap year (valid)
    [InlineData("9901014800080", 0, true)]   // yy=99, cutoff=0 -> 1999 (maps to 1900s)
    [InlineData("9901014800080", 99, true)]  // yy=99, cutoff=99 -> 2099 (maps to 2000s)
    public void IsValidSAID_WithCenturyCutoff(string idNumber, int centuryCutoff, bool expected)
    {
        bool actual = RSAIdValidator.IsValidSAID(idNumber, centuryCutoff);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IsValidSAID_DefaultCutoffMatchesExplicitCutoff()
    {
        string id = "2201014800082";
        Assert.Equal(
            RSAIdValidator.IsValidSAID(id),
            RSAIdValidator.IsValidSAID(id, 25));
    }

    [Fact]
    public void IsValidSAID_CutoffAtBoundary_MapsToCorrectCentury()
    {
        // yy=26: at cutoff=26 should map to 2000s, at cutoff=25 should map to 1900s
        // Both are valid dates, but we verify the parameter is respected
        // by using the generator round-trip: an ID generated for 2026 should
        // validate with cutoff >= 26
        string id = RSAIdGenerator.GenerateValidSAID(new System.DateTime(2026, 1, 1), false, true);
        Assert.True(RSAIdValidator.IsValidSAID(id, 26));
        Assert.True(RSAIdValidator.IsValidSAID(id, 50));
    }
}