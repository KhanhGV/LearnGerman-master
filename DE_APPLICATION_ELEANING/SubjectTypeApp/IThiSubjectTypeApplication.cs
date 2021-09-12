using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.SubjectTypeApp
{
    interface IThiSubjectTypeApplication
    {
        Task<int> DeleleListAsync(string[] List);
        Task<int> AppvoreListAsync(string[] List, bool status);
        Task<int> Appvore(Guid Id, bool status);
    }
}
