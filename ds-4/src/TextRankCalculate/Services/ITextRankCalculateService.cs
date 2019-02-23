using TextRankCalculate.Models;

namespace TextRankCalculate.Services
{
    public interface ITextRankCalculateService
    {
        TextRank calculate(string text);
    }
}