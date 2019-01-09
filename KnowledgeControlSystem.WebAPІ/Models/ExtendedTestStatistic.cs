using System;

namespace KnowledgeControlSystem.WebAPІ.Models
{
    public class ExtendedTestStatistic
    {
        public TimeSpan AverageTime { get; set; }
        public double AverageProcent { get; set; }
        public int PassedTestCount{get;set;}
    }
}