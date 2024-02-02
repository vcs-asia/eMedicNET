using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;

namespace eMedicNETv7.Extensions
{
    public static class KeyGenerator
    {
        public static string RandomSpecialString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!#$%&*@[]{}|()_-0123456789";
            return new string(Enumerable.Repeat(chars, 78).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Get()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomSpecialString());

            return builder.ToString();
        }
    }
}

