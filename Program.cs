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
            ConsoleColor oldColor = Console.ForegroundColor;

            try
            {
                Console.WriteLine("Please write your desired nickname.");
                string nick = Console.ReadLine();

                if (nick == null || nick == string.Empty || nick.Length == 0)
                {
                    Console.WriteLine("Passing an empty name may cause issues, or even being banned when joining a server!\nMake sure to enter a valid non-empty name with no special characters.");
                    Console.Read();
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
