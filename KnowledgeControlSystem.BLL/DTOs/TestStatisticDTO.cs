using System;

namespace KnowledgeControlSystem.BLL.DTOs
{
    public class TestStatisticDTO
    {
        public string UserName { get; set; }
        public int PassedTestCount { get; set; }
        public double AvgScorePercent { get; set; }
        public double AvgTimeSeconds { get; set; }
    }
}
