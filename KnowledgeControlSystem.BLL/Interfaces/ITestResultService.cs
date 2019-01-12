using System.Collections.Generic;
using KnowledgeControlSystem.BLL.DTOs;

namespace KnowledgeControlSystem.BLL.Interfaces
{
    public interface ITestResultService:IService<TestResultDTO>
    {
        List<TestResultDTO> FindByUser(int userId);
    }
}