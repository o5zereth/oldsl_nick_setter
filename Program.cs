using Microsoft.Win32;
using System;
using System.Text;

namespace oldsl_nick_setter
{
    class Program
    {
        private const string keyName = @"HKEY_CURRENT_USER\Software\Hubert Moszka\SCP: Secret Laboratory";
        private const string keyName_2 = @"HKEY_CURRENT_USER\Software\Hubert Moszka\SCPSL";
        private const string subKeyName = @"Software\Hubert Moszka\SCP: Secret Laboratory";
        private const string subKeyName_2 = @"Software\Hubert Moszka\SCPSL";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please write your desired nickname.");
                string nick = Console.ReadLine();

                int totalNonWhiteSpace = 0;

                for (int i = 0; i < nick.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace($"{nick[i]}"))
                    {
                        totalNonWhiteSpace++;
                    }
                }

                if (string.IsNullOrWhiteSpace(nick) || totalNonWhiteSpace < 3 || nick.Length > 25)
                {
                    WriteLineColor("The entered nickname is not allowed.", ConsoleColor.DarkRed);
                    WriteLineColor("Make sure the nickname is between 3-25 characters and is not whitespace only.", ConsoleColor.Magenta);
                    
                    Console.WriteLine("\nPress any key to exit.");
                    Console.ReadKey();
                    return;
                }

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

                Registry.SetValue(keyName, "nickname", Encoding.ASCII.GetBytes(nick));
                Registry.SetValue(keyName_2, "nickname", Encoding.ASCII.GetBytes(nick));

                WriteLineColor("Nickname set succssfully!", ConsoleColor.Green);
            }
            catch (Exception e)
            {
                WriteLineColor(e.ToString(), ConsoleColor.DarkRed);
            }

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
