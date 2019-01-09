using KnowledgeControlSystem.BLL.DTOs;

namespace KnowledgeControlSystem.BLL.Interfaces
{
    public interface IStatisticService
    {
        TestStatisticDTO GetStatistics(int userId);
    }
}
