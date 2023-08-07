using System;
using System.Text;

namespace UehInternFrontend
{
    public class Cipher
    {
        private const int Shift = 3;

        private static string AddRandomNumbers(string plaintext)
        {
            var random = new Random();
            StringBuilder textWithRandomNumbers = new StringBuilder();

            foreach (char c in plaintext)
            {
                textWithRandomNumbers.Append(c);
                if (char.IsDigit(c))
                {
                    textWithRandomNumbers.Append(random.Next(10)); // Insert a random number between 0 and 9
                }
            }

            return textWithRandomNumbers.ToString();
        }

        private static string RemoveRandomNumbers(string textWithRandomNumbers)
        {
            StringBuilder plaintext = new StringBuilder();
            bool skipNext = false;

            foreach (char c in textWithRandomNumbers)
            {
                if (skipNext)
                {
                    skipNext = false;
                    continue; // Skip the random number
                }

                plaintext.Append(c);

                if (char.IsDigit(c))
                {
                    skipNext = true;
                }
            }

            return plaintext.ToString();
        }

        public static string Encrypt(string plaintext)
        {
            // Text with Random Numbers
            plaintext = Cipher.AddRandomNumbers(plaintext);

            var random = new Random();
            int randomShift = random.Next(1, 10); // Random shift between 1 and 9

            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in plaintext)
            {
                if (char.IsDigit(c))
                {
                    char encryptedChar = (char)(((c - '0' + Shift + randomShift) % 10) + '0');
                    encryptedText.Append(encryptedChar);
                }
                else
                {
                    encryptedText.Append(c);
                }
            }

            // Append the random shift as the 4th character
            encryptedText.Insert(3, randomShift.ToString());

            return encryptedText.ToString();
        }

        public static string Decrypt(string ciphertext)
        {
            int randomShift = int.Parse(ciphertext[3].ToString()); // Get the random shift from the 4th character

            StringBuilder decryptedText = new StringBuilder();
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (i == 3)
                {
                    continue;
                }

                char c = ciphertext[i];
                if (char.IsDigit(c))
                {
                    char decryptedChar = (char)(((c - '0' - Shift + 10 - randomShift) % 10) + '0');
                    decryptedText.Append(decryptedChar);
                }
                else
                {
                    decryptedText.Append(c);
                }
            }

            string decryptedPlaintext = Cipher.RemoveRandomNumbers(decryptedText.ToString());
            return decryptedPlaintext;
        }

    // Cipher.Encrypt(plaintext);
    // Cipher.Decrypt(encryptedText);

    }
}