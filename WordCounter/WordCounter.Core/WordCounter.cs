using System;
using System.Collections.Generic;
using System.Linq;
using WordCounter.Core.Extensions;
using WordCounter.Core.Interfaces;

namespace WordCounter.Core
{
    public class WordCounter : IWordCounter
    {
        private readonly int _returnNumber = 20;

        private readonly Dictionary<string, int> _wordCount;

        public WordCounter()
        {
            _wordCount = new Dictionary<string, int>();
        }

        public Dictionary<string, int> CountWords(string text)
        {
            var words = FormatTextForWordCount(text);

            foreach (var word in words)
            {
                if (_wordCount.ContainsKey(word))
                {
                    _wordCount[word]++;
                }
                else
                {
                    _wordCount.Add(word, 1);
                }
            }

            var orderedList = _wordCount.OrderByDescending(value => value.Value).Take(_returnNumber).ToDictionary(pair => pair.Key, pair => pair.Value);

            return orderedList;
        }

        private static string[] FormatTextForWordCount(string text)
        {
            // Remove non-alphanumeric characters (tr -cs 'a-zA-Z' '[\n*])'
            text = text.RemoveNonAlphanumericCharacters();

            // Convert text to lowercase so that for example "The" and "the" are no treated as two separate words (tr '[:upper:]' '[:lower:]')
            text = text.ToLower();

            // Split the words into a string array ignoring elements that contain an empty string
            var words = text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}