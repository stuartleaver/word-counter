using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Core.Extensions;
using WordCounter.Core.Interfaces;

namespace WordCounter.Core
{
    public class WordCounter : IWordCounter
    {
        private readonly int _returnNumber = 20;

        private readonly Dictionary<string, int> _wordCount;

        private readonly object _dictionaryLock;

        public WordCounter()
        {
            _wordCount = new Dictionary<string, int>();

            _dictionaryLock = new object();
        }

        /// <summary>
        /// Counts the words contained within a string word by word
        /// </summary>
        /// <param name="text">Text containing the words to count</param>
        /// <returns>Dictionary&lt;string, int&gt;</returns>
        public Dictionary<string, int> CountWords(string text)
        {
            var words = FormatTextForWordCount(text);

            foreach (var word in words)
            {
                _wordCount.TryGetValue(word, out var count);

                _wordCount[word] = count + 1;
            }

            var orderedList = _wordCount.OrderByDescending(value => value.Value).Take(_returnNumber).ToDictionary(pair => pair.Key, pair => pair.Value);

            return orderedList;
        }

        /// <summary>
        /// Counts the contained within a string word by word for each line.
        /// </summary>
        /// <param name="text">Text containing the words to count</param>
        /// <returns>Dictionary&lt;string, int&gt;</returns>
        public Dictionary<string, int> CountWordsByLine(string text)
        {
            var lines = text.Split('\n');

            Parallel.ForEach(lines, line =>
                         {
                             var words = FormatTextForWordCount(line);

                             foreach (var word in words)
                             {
                                 lock (_dictionaryLock)
                                 {
                                     _wordCount.TryGetValue(word, out var count);

                                     _wordCount[word] = count + 1;
                                 }
                             }
                         });

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
            var words = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}