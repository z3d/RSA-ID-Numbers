using RsaIdNumbers;

if (args.Length == 0)
{
    System.Console.WriteLine("Usage: RsaIdNumbers.Console <command> [options]");
    System.Console.WriteLine("Commands:");
    System.Console.WriteLine("  generate [birthdate:yyyy-MM-dd] [gender:male|female] [citizenship:citizen|non-citizen]");
    System.Console.WriteLine("  validate <id>");
    return;
}

string command = args[0].ToLower();

switch (command)
{
    case "generate":
        HandleGenerateCommand(args);
        break;
    case "validate":
        HandleValidateCommand(args);
        break;
    default:
        Console.WriteLine($"Unknown command: {command}");
        break;
}

void HandleGenerateCommand(string[] args)
{
    DateTime birthDate;
    bool isFemale = true;
    bool isCitizen = true;

    if (args.Length > 1)
    {
        if (!DateTime.TryParse(args[1], out birthDate))
        {
            Console.WriteLine("Invalid birthdate format. Use yyyy-MM-dd.");
            return;
        }
    }
    else
    {
        // Generate a random birthdate within the last 75 years
        DateTime maxDate = DateTime.Now;
        DateTime minDate = maxDate.AddYears(-75);
        birthDate = GenerateRandomDate(minDate, maxDate);
    }

    if (args.Length > 2)
    {
        string genderInput = args[2].ToLower();
        if (genderInput == "female")
        {
            isFemale = true;
        }
        else if (genderInput == "male")
        {
            isFemale = false;
        }
        else
        {
            Console.WriteLine("Invalid gender. Use 'male' or 'female'.");
            return;
        }
    }

    if (args.Length > 3)
    {
        string citizenshipInput = args[3].ToLower();
        if (citizenshipInput == "citizen")
        {
            isCitizen = true;
        }
        else if (citizenshipInput == "non-citizen")
        {
            isCitizen = false;
        }
        else
        {
            Console.WriteLine("Invalid citizenship. Use 'citizen' or 'non-citizen'.");
            return;
        }
    }

    string generatedId = RSAIdGenerator.GenerateValidSAID(birthDate, isFemale, isCitizen);
    Console.WriteLine($"Generated ID: {generatedId}");
}

// Generates a random date between the specified start and end dates
DateTime GenerateRandomDate(DateTime start, DateTime end)
{
    int range = (end - start).Days;
    return start.AddDays(new Random().Next(range));
}

void HandleValidateCommand(string[] args)
{
    if (args.Length < 2)
    {
        Console.WriteLine("Usage: validate <id>");
        return;
    }

    string id = args[1];
    bool isValid = RSAIdValidator.IsValidSAID(id);
    Console.WriteLine(isValid ? "Valid ID" : "Invalid ID");
}
