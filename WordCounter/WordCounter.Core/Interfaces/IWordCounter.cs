﻿using System.Collections.Generic;

namespace WordCounter.Core.Interfaces
{
    public interface IWordCounter
    {
        Dictionary<string, int> CountWords(string text);

        Dictionary<string, int> CountWordsByLine(string text);
    }
}