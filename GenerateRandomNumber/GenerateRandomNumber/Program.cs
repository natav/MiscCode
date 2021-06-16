using System;
using System.Linq;
using System.Text;

namespace GenerateRandomNumber
{
    class RandomNumberSample
    {
        static void Main(string[] args)
        {
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            Console.WriteLine($"Random number between 5 and 100 is {randomNumber}");

            var randomString = generator.RandomString(10);
            Console.WriteLine($"Random string of 10 chars is {randomString}");

            var randomPassword = generator.RandomPassword();
            Console.WriteLine($"Random string of 10 chars is {randomPassword}");

            Console.WriteLine("Length of a passsword is: " + "A85735DA18F71A6848F24CC850FE9DBCB7F3DAB5639858C6B1EA0D0959CE3958".Length.ToString());

            var randomStringLettersAndNumbers = generator.RandomStringLettersAndNumbers(64);
            Console.WriteLine($"Random string of 64 chars is {randomStringLettersAndNumbers}");

            var randomStringLettersAndNumbers1 = generator.RandomStringLettersAndNumbers(64);
            Console.WriteLine($"Random string of 64 chars is {randomStringLettersAndNumbers1}");

            var randomStringLettersAndNumbers2 = generator.RandomStringLettersAndNumbers(64);
            Console.WriteLine($"Random string of 64 chars is {randomStringLettersAndNumbers2}");

            var randomStringLettersAndNumbers3 = generator.RandomStringLettersAndNumbers(64);
            Console.WriteLine($"Random string of 64 chars is {randomStringLettersAndNumbers3}");

            var randomStringLettersAndNumbers4 = generator.RandomStringLettersAndNumbers(64);
            Console.WriteLine($"Random string of 64 chars is {randomStringLettersAndNumbers4}");

            Console.ReadKey();
        }
    }

    public class RandomGenerator
    {
        // Instantiate random number generator.  
        // It is better to keep a single Random instance 
        // and keep using Next on the same instance.  
        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        // Generates a random password.  
        // 4-LowerCase + 4-Digits + 2-UpperCase  
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }
        public string RandomStringLettersAndNumbers(int length)
        {
            const string pool = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[_random.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }
    }
}  