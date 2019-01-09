using System;

namespace KnowledgeControlSystem.BLL.DTOs
{
    public class TestResultDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int TotalScore { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
