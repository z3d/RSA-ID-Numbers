# RSA ID Numbers

RSA ID Numbers is a .NET library for working with South African ID numbers. It can generate and validate South African ID numbers.

## Installation

RSA ID Numbers can be installed via NuGet. Run the following command in the Package Manager Console:

```Install-Package RSAIdNumbers```

## Usage

### Validating an ID Number

To validate a South African ID number, use the `RSAIdValidator` class:

```csharp
using RsaIdNumbers;

bool isValid = RSAIdValidator.IsValidSAID("7201014800087");
```

### Generating a Valid ID Number

To generate a valid South African ID number, use the `RSAIdGenerator` class:

```csharp
using RsaIdNumbers;

// Generate an ID with default parameters
string id = RSAIdGenerator.GenerateValidSAID();

// Generate an ID with specific parameters
DateTime birthDate = new DateTime(1990, 5, 15);
bool isFemale = true;
bool isCitizen = true;
string customId = RSAIdGenerator.GenerateValidSAID(birthDate, isFemale, isCitizen);
```

### Command-Line Usage

The `RsaIdNumbers.ConsoleApp` application uses the RSA ID Numbers library and provides the following commands:

- **Generate an ID**:
  ```bash
  dotnet run generate [birthdate:yyyy-MM-dd] [gender:male|female] [citizenship:citizen|non-citizen]
  ```
  Example:
  ```bash
  dotnet run generate 1990-05-15 female citizen
  ```
  - `birthdate`: The date of birth in `yyyy-MM-dd` format. If omitted, a random date within the last 75 years is used.
  - `gender`: Specify `male` or `female`. Defaults to `female` if omitted.
  - `citizenship`: Specify `citizen` or `non-citizen`. Defaults to `citizen` if omitted.

- **Validate an ID**:
  ```bash
  dotnet run validate <id>
  ```
  Example:
  ```bash
  dotnet run validate 7201014800087
  ```
  - `<id>`: The South African ID number to validate.

## References

For more information on what a South African ID number means and what it reveals about you, [see this article](https://mybroadband.co.za/news/security/303812-what-your-south-african-id-number-means-and-what-it-reveals-about-you.html).

## License

RSA ID Numbers is licensed under the MIT License.
