using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DE_APPLICATION_ELEANING.WordApp
{
    interface IWordApplication
    {
        Task<int> DeleleListAsync(string[] List);
        Task<int> AppvoreListAsync(string[] List, bool status);
        Task<int> Appvore(Guid Id, bool status);
        Task<List<Word>> GetRamDomFailseAnswer(int count, Guid guid);
    }
}
