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
}