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

                Console.Write("Nickname set succssfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: \n" + e.Message);
            }

            Console.Read();
        }
    }
}
