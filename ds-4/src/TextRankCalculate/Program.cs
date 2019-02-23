using StackExchange.Redis;
using System;
using TextRankCalculate.Models;
using TextRankCalculate.Services;
using TextRankCalculate;

namespace TextRankCalculate
{
    class Program
    {
        static void Main(string[] args)
        {
            Application application = new Application(new TextRankCalculateService());
            application.Run();
        }
    }
}
