# RSA ID Numbers

RSA ID Numbers is a .NET library for working with South African ID numbers. It can extract information such as date of birth, gender, and citizenship from a valid South African ID number.

## Installation

RSA ID Numbers can be installed via NuGet. Run the following command in the Package Manager Console:

```Install-Package RSA-ID-Numbers```


## Usage

To extract information from a South African ID number, use the `RSAIDNumber` class:

```csharp
using RSA_ID_Numbers;

// create a new RSAIDNumber instance
var idNumber = new RSAIDNumber("9208121234087");

For more information on the properties and methods available in the RSAIDNumber class, see the documentation.

References
For more information on what a South African ID number means and what it reveals about you, see this article.

License
RSA ID Numbers is licensed under the MIT License.
