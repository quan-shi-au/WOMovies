using System;

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

        public static string ValidateYear(this string input)
        {
            var year = DateTime.Now.Year;

            if (string.IsNullOrEmpty(input))
                return DateTime.Now.Year.ToString();

            return int.TryParse(input, out year) ? year.ToString() : DateTime.Now.Year.ToString();
        }

    }
}
