using DE_DB_ELEANING.AFModel;
using System;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.StudentApp
{
    interface IStudentApplication
    {
        Task<Student> GetByPhone(string phone);
        Task<Student> GetByEmail(string phone);

        Task<int> DeleleListAsync(string[] List);
        Task<int> AppvoreListAsync(string[] List, bool status);
        Task<int> Appvore(Guid Id, bool status);
    }
}
