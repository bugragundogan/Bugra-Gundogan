using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CSE105FinalProject
{
    public static class publicFunctions
    {
        public static char crSeperator = ':';

        public static bool checkUsername(string inputUN)
        {
            bool blExists = false;
            foreach (var vrLine in File.ReadLines("users.txt"))
            {
                if (vrLine.Split(crSeperator)[0] == inputUN)
                {
                    blExists = true;
                    break;
                }

            }
            return blExists;
        }

        public static bool checkPw(string inputUN,string inputPW)
        {
            string hashedPW = publicFunctions.ComputeSha256Hash(inputPW);
            bool blCorrectPw = false;
            foreach (var vrLine in File.ReadLines("users.txt"))
            { 
                var splitP = vrLine.Split(crSeperator);
                if (splitP[0]==inputUN)
                {
                    if (splitP[1] == hashedPW)
                    {
                        blCorrectPw = true;
                    }
                }
            }
            return blCorrectPw;
        }

        public static string trToEn(string a)
        {
            char[] trHarf = { 'ö', 'ü', 'ç', 'ğ', 'ş', 'ı' };
            char[] enHarf = { 'o', 'u', 'c', 'g', 's', 'i' };
            for (int i = 0; i < trHarf.Length; i++)
            {
                a = a.Replace(trHarf[i], enHarf[i]);
            }
            return a;
        }

        public static bool doesntStartWithANumber(string a)
        {
            bool doesntStart = false;
            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                
                if (a.StartsWith(i.ToString()))
                {
                    doesntStart = false;
                }
                else
                {
                    j++;
                }
            }
            if (j == 10)
            {
                doesntStart = true;
            }

            return doesntStart;
        }
       
        public static string ComputeSha256Hash(string rawData)
        {
                // Create a SHA256   
           using (SHA256 sha256Hash = SHA256.Create())
           {
                    // ComputeHash - returns byte array  
              byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                    // Convert byte array to a string   
              StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
           }
        }
        
        public static bool operasyonKullanildi(string a)
        {
            bool kullanildi;
            if(a=="Please enter your operation here...")
            {
                kullanildi = false;
            }
            else
            {
                kullanildi = true;
            }
            return kullanildi;
        }


    }
}
