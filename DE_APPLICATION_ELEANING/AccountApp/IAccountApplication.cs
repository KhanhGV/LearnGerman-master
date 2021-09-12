using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.AccountApp
{
    interface IAccountApplication<TModel>
    {
        Task<TModel> GetByUserName(string username);
        Photo GetPhoByUserName(string username);
        bool UpdatePhoto(string usernmae,string url);
    }
}
