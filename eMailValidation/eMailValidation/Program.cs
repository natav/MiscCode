using System;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Diagnostics;

public class Utilities
{
    public static bool IsValidEmail(string email, bool Legistar = false)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            if (Legistar)
            {
                return Regex.IsMatch(email,
                   @"^[a-zA-Z0-9][\w/_\.'-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$",
                    RegexOptions.IgnoreCase);
            }
            else
            {
                // see https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase);
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("----------------------------------");
        Console.WriteLine("using new REGEX");
        Debug.WriteLine("using new REGEX");
        Console.WriteLine("----------------------------------");

        string[] emailAddresses = { "john.smith@clientdomain.com", "j.s@server1.clientdomain.com",
                                    "smith@ms1.clientdomain.com", "j.@server1.clientdomain.com", "j/s@clientdomain.com",
                                    "j@clientdomain.com9", "js#internal@clientdomain.com", "j}s@clientdomain.com",
                                    "j_9@[129.126.118.1]", "j..s@clientdomain.com","j{s@clientdomain.com","[js]@clientdomain.com", "{js@clientdomain.com",
                                    "js*@clientdomain.com", "js@clientdomain..com", "j$s@clientdomain.com", ".js@clientdomain.com", "j-s@clientdomain.com", "j~s@clientdomain.com",
                                    "js.@clientdomain.com", "j!s@clientdomain.com", "j#s@clientdomain.com", "j%s@clientdomain.com", "j^s@clientdomain.com", "j|s@clientdomain.com",
                                    "j&s@clientdomain.com", "j*s@clientdomain.com", "j(s@clientdomain.com", "j=s@clientdomain.com", "j?s@clientdomain.com",
                                    "js@clientdomain.com9", "j.s@server1.clientdomain.com", "O'smith@ms1.clientdomain.com", "john`smith@ms1.clientdomain.com",
                                    "\"j\\\"s\\\"\"@clientdomain.com", "js@clientdomain.12", "john+smith@ms1.clientdomain.com" };

        foreach (var emailAddress in emailAddresses)
        {
            if (Utilities.IsValidEmail(emailAddress))
            {
                Console.WriteLine($"Valid:   {emailAddress}");
                Debug.WriteLine($"Valid:   {emailAddress}");
            }
            else
            { 
                Console.WriteLine($"Invalid: {emailAddress}");
                Debug.WriteLine($"Invalid: {emailAddress}");
            }
        }

        Console.WriteLine("----------------------------------");
        Console.WriteLine("using System.Net.Mail.MailAddress");
        Debug.WriteLine("using System.Net.Mail.MailAddress");
        Console.WriteLine("----------------------------------");

        foreach (var emailAddress in emailAddresses)
        {
            try
            {
                MailAddress mail = new MailAddress(emailAddress);
                Console.WriteLine($"Valid:   {emailAddress}");
                Debug.WriteLine($"Valid:   {emailAddress}");
            }
            catch
            {
                Console.WriteLine($"Invalid:   {emailAddress}");
                Debug.WriteLine($"Invalid:   {emailAddress}");
            } 
        }

        Console.WriteLine("----------------------------------");
        Console.WriteLine("using Legistar's current REGEX (plus single quote support)");
        Debug.WriteLine("using Legistar's current REGEX (plus single quote support)");
        Console.WriteLine("----------------------------------");

        foreach (var emailAddress in emailAddresses)
        {
            if (Utilities.IsValidEmail(emailAddress, true))
            { 
                Console.WriteLine($"Valid:   {emailAddress}");
                Debug.WriteLine($"Valid:   {emailAddress}");
            }
            else
            {
                Console.WriteLine($"Invalid: {emailAddress}");
                Debug.WriteLine($"Invalid: {emailAddress}");
            }
        }
        Console.ReadKey();
    }
}