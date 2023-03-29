# RSA ID Numbers

RSA ID Numbers is a .NET library for working with South African ID numbers. It can validate a South African ID number.

## Installation

RSA ID Numbers can be installed via NuGet. Run the following command in the Package Manager Console:

```Install-Package RSAIdNumbers```


## Usage

To extract information from a South African ID number, use the `RSAIDNumber` class:

```csharp
using RsaIdNumbers;
bool isValid = RSAIdValidator.IsValidSAID("7201014800087");
```

References
For more information on what a South African ID number means and what it reveals about you, [see this article](https://mybroadband.co.za/news/security/303812-what-your-south-african-id-number-means-and-what-it-reveals-about-you.html).

License
RSA ID Numbers is licensed under the MIT License.
