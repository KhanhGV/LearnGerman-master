using DE_APPLICATION_ELEANING.QuestionApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DE_APPLICATION_ELEANING.QuestionApp
{
    interface IQuestionApplication
    {
        Task<int> DeleleListAsync(string[] List);
        Task<int> AppvoreListAsync(string[] List, bool status);
        Task<int> Appvore(Guid Id, bool status);
        Task<bool> CreateVocabularyResult(VocabularyResult input);
        Task<bool> UpdateVocabularyResult(VocabularyResult input);
        Task<List<VocabularyResult>> GetVocabularyResult(SearchAnswerDto input);
    }
}
