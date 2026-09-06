using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzPass
{
    static class PasswordGenerator
    {
        /// <summary>
        /// Create a new random password
        /// </summary>
        /// <param name="wordCount"></param>
        /// <returns></returns>
        public static string New(int wordCount, bool useNumbers, bool useReplacement, int determinedRandomNumber = 0)
        {
            string pass = string.Empty;

            // Grab random words from wordlist
            for (int i = 0; i < wordCount; i++)
            {
                pass += FetchRandomWord(); 
            }

            // Replace characters with their symbol counterparts, found in WordList.cs / replacementChars dictionary
            if (useReplacement)
                ReplaceCharacters(ref pass);

            // Add numbers to the end, followed by a random (rather than fixed) easily-typable symbol
            if (useNumbers)
            {
                if (determinedRandomNumber == 0)
                    pass += $".{SecureRandom.Next(10, 1000)}{FetchRandomSymbol()}";
                else
                    // Else if there is already a pre-determined random number (This is only used by the bulk generation window)
                    pass += $".{determinedRandomNumber}{FetchRandomSymbol()}";
            }

            return pass;
        }

        /// <summary>
        /// Fetches a random word from the wordlist
        /// </summary>
        /// <returns></returns>
        private static string FetchRandomWord()
        {
            // Get random word, convert it to lowercase then uppercase the first character (first character won't be turned into a symbol)
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(WordList.shortList[SecureRandom.Next(0, WordList.shortList.Count)].ToLower());
        }

        /// <summary>
        /// Fetches a random easily-typable symbol used for the password suffix, found in
        /// WordList.cs / suffixSymbols
        /// </summary>
        /// <returns></returns>
        private static char FetchRandomSymbol()
        {
            return WordList.suffixSymbols[SecureRandom.Next(0, WordList.suffixSymbols.Length)];
        }

        /// <summary>
        /// Replaces characters with their symbol counterparts for improved password security without sacrificing readability
        /// Conversion list defined in WordList.cs / replacementChars dictionary
        /// </summary>
        /// <param name="pass"></param>
        public static void ReplaceCharacters(ref string pass)
        {
            // StringBuilder for easy character swapping
            StringBuilder b = new StringBuilder(pass);
            // Switch to make sure two characters are not symbolised next to each other (this ruins the readability)
            bool s = false;
            // Character indexing
            int i = 0;
            // List of characters already converted
            List<char> usedChars = new List<char>();

            // Loop through every character in the generated password
            foreach(var entry in pass)
            {
                // If replacement list contains this character
                if (WordList.replacementChars.TryGetValue(entry, out char newChar))
                {
                    // If switch is set to false
                    if (!s)
                    {
                        // Check against the original (pre-replacement) character - usedChars must be
                        // populated with the same before this guard can ever match
                        if (!usedChars.Contains(entry))
                        {
                            // Replace character with symbol
                            b[i] = newChar;
                            // Cache the original character as already converted
                            usedChars.Add(entry);
                        }
                    }

                    // Set switch to false if previous character was symbolised
                    s = !s;
                }
                else
                {
                    // Disable switch on next character that doesn't have a symbol counterpart
                    if (s)
                        s = !s;
                }

                // Increase index
                i++;
            }

            // Update password
            pass = b.ToString().TrimEnd();
        }

        public static void InvertReplaceCharacters(ref string pass)
        {
            // Invert word list keys and values
            Dictionary<char, char> replacementChars = new Dictionary<char, char>();
            foreach (var entry in WordList.replacementChars)
            {
                replacementChars.Add(entry.Value, entry.Key);
            }

            // StringBuilder for easy character swapping
            StringBuilder b = new StringBuilder(pass);
            // Character indexing
            int i = 0;
            // List of characters already converted
            List<char> usedChars = new List<char>();

            // Loop through every character in the generated password
            foreach (var entry in pass)
            {
                // If replacement list contains this character
                if (replacementChars.TryGetValue(entry, out char newChar))
                {
                    // Check against the original (pre-replacement) character - see ReplaceCharacters above
                    if (!usedChars.Contains(entry))
                    {
                        // Replace character with symbol
                        b[i] = newChar;
                        // Cache the original character as already converted
                        usedChars.Add(entry);
                    }
                }

                // Increase index
                i++;
            }

            // Update password
            pass = b.ToString().TrimEnd();
        }
    }
}
