using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EzPass
{
    public static class RegHandler
    {
        public static object ReadKey(string valueName)
        {
            // Only read access is required here - writable access was previously requested
            // even for reads, which violates least privilege
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\EzPass", false) ?? CreateNewTemplatedKey())
            {
                return key.GetValue(valueName);
            }
        }

        public static void WriteKey(string valueName, object obj)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\EzPass", true) ?? CreateNewTemplatedKey())
            {
                key.SetValue(valueName, obj, RegistryValueKind.String);
            }
        }

        private static RegistryKey CreateNewTemplatedKey()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EzPass", true);
            key.SetValue("Version", WordList.version.ToString(), RegistryValueKind.String);
            key.SetValue("Wordlist", WordlistHandler.ToRegistryValue(WordList.shortList));

            return key;
        }

        public static class WordlistHandler
        {
            public static string ToRegistryValue(List<string> list)
            {
                string x = string.Join(";", list);
                x = Regex.Replace(x, @"\t|\n|\r", "");      // Sanitise string before (remove new lines, returns etc) to make sure the registry entry is clean
                return x;
            }

            public static List<string> FromRegistryValue(object obj)
            {
                string x = obj.ToString();
                x = Regex.Replace(x, @"\t|\n|\r", "");
                return x.Split(';').ToList();
            }
        }
    }
}
