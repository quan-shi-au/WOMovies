using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Infrastructure
{
    public static class StringExtension
    {
        public static string EscapeString(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Replace("'", "\"");
        }
    }
}
