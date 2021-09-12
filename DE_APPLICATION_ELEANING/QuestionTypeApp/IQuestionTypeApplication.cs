using System;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.QuestionTypeApp
{
    interface IQuestionTypeApplication
    {
        Task<int> DeleleListAsync(string[] List);
        Task<int> AppvoreListAsync(string[] List, bool status);
        Task<int> Appvore(Guid Id, bool status);
    }
}
