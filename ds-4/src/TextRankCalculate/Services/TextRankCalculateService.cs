using System;
using System.Collections.Generic;
using System.Text;
using TextRankCalculate.Models;

namespace TextRankCalculate.Services
{
    public class TextRankCalculateService : ITextRankCalculateService
    {
        public TextRank calculate(string text)
        {
            var textRank = new TextRank();

            foreach (var ch in text)
            {
                if (isVowelsChar(ch))
                {
                    textRank.CountVowels++;
                }
                else if (!isSpecialChar(ch))
                {
                    textRank.CountConsonants++;
                }
            }

            return textRank;
        }

        private static bool isSpecialChar(char ch)
        {
            return (ch == 't' || ch == '\0' || ch == ' ');
        }

        private static bool isVowelsChar(char ch)
        {
            return (ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u') ||
                                (ch == 'A' || ch == 'E' || ch == 'I' || ch == 'O' || ch == 'U');
        }
    }
}
