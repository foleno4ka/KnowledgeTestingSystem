using System;

namespace KnowledgeControlSystem.WebAPІ.Models
{
    public class ResultViewModel
    {
        public DateTime PassDate { get; set; }
        public long Duration { get; set; }
        public int Score { get; set; }
        public int TotalScore { get; set; }
        public double PassingProcent { get; set; }
    }
}