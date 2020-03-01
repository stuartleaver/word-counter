using System;
using System.Text.RegularExpressions;

namespace WordCounter.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove non a-z characters replacing with a new line (tr -cs 'a-zA-Z' '[\n*]')
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public static string RemoveNonAlphanumericCharacters(this string text)
        {
            var expression = new Regex(@"[^a-zA-Z]");

            return expression.Replace(text, Environment.NewLine);
        }
    }
}