using System;
using System.Collections.Generic;
using System.Text;

namespace TextRankCalculate.Models
{
    public class TextRank
    {
        public int CountVowels { get; set; } = 0;
        public int CountConsonants { get; set; } = 0;

        public override string ToString()
        {
            return "TextRank: " +  CountVowels + ": " + CountConsonants;
        }
    }
}
