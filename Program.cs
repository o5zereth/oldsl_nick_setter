using Microsoft.Win32;
using System;
using System.Text;

namespace oldsl_nick_setter
{
    class Program
    {
        // Registry keynames for oldest versions of SCP:SL.
        private const string keyName = @"HKEY_CURRENT_USER\Software\Hubert Moszka\SCP: Secret Laboratory";
        private const string subKeyName = @"Software\Hubert Moszka\SCP: Secret Laboratory";

        // Registry keynames for older versions of SCP:SL.
        private const string keyName_2 = @"HKEY_CURRENT_USER\Software\Hubert Moszka\SCPSL";
        private const string subKeyName_2 = @"Software\Hubert Moszka\SCPSL";

        static void Main(string[] args)
        {
            try
            {
                // Get the written nickname.
                Console.WriteLine("Please write your desired nickname.");
                string nick = Console.ReadLine();

                // Get the number of total non-whitespace characters.
                int totalNonWhiteSpace = 0;
                for (int i = 0; i < nick.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace($"{nick[i]}"))
                    {
                        totalNonWhiteSpace++;
                    }
                }

                // Don't allow nicknames that are empty, contain less than 3 non-whitespace characters, or exceed 25 characters in length.
                if (string.IsNullOrWhiteSpace(nick) || totalNonWhiteSpace < 3 || nick.Length > 25)
                {
                    // Tell user what is allowed.
                    WriteLineColor("The entered nickname is not allowed.", ConsoleColor.DarkRed);
                    WriteLineColor("Make sure the nickname is between 3-25 characters and is not whitespace only.", ConsoleColor.Magenta);

                    // Exit the program on key-press.
                    Console.WriteLine("\nPress any key to exit.");
                    Console.ReadKey();
                    return;
                }

                // Deletes all currently saved nicknames for oldest versions of SCP:SL.
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyName, true))
                {
                    if (key != null)
                    {
                        string[] keyNames = key.GetValueNames();

                        for (int i = 0; i < keyNames.Length; i++)
                        {
                            if (keyNames[i].ToLower().Contains("nickname"))
                            {
                                key.DeleteValue(keyNames[i]);
                            }
                        }
                    }
                }

                // Deletes all currently saved nicknames for older versions of SCP:SL.
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyName_2, true))
                {
                    if (key != null)
                    {
                        string[] keyNames = key.GetValueNames();

                        for (int i = 0; i < keyNames.Length; i++)
                        {
                            if (keyNames[i].ToLower().Contains("nickname"))
                            {
                                key.DeleteValue(keyNames[i]);
                            }
                        }
                    }
                }

                // Add given nickname to both old SCP:SL keynames.
                Registry.SetValue(keyName, "nickname", Encoding.ASCII.GetBytes(nick));
                Registry.SetValue(keyName_2, "nickname", Encoding.ASCII.GetBytes(nick));

                // Tell user the nickname was set.
                WriteLineColor("Nickname set succssfully!", ConsoleColor.Green);
            }
            catch (Exception e) // If an error occurs, then log it.
            {
                WriteLineColor(e.ToString(), ConsoleColor.DarkRed);
            }

            // Exit the program on key-press.
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        static void WriteLineColor(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }
    }
}
