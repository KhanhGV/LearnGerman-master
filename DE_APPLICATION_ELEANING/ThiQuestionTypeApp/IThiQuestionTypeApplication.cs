﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiQuestionTypeApp
{
    interface IThiQuestionTypeApplication
    {
        Task<int> DeleleListAsync(string[] List);
        Task<int> AppvoreListAsync(string[] List, bool status);
        Task<int> Appvore(Guid Id, bool status);
    }
}
