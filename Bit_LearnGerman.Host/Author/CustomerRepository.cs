using DE_APPLICATION_ELEANING.StudentApp;
using DE_DB_ELEANING.AFModel;
using DE_DB_ELEANING.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
namespace Oauth_2._0_v2.Author
{
    public class CustomerRepository: IDisposable
    {
        private readonly StudentApplication _studentApp;
        public CustomerRepository()
        {
            
            if (_studentApp == null)
            {
                _studentApp = new StudentApplication();
            }
        }
        public async Task<Student> ValidateUser(string username)
        {
            try
            {
                var _sign = await  _studentApp.GetByPhone(username);
                if(_sign==null)
                {
                    _sign = _studentApp.GetByEmail(username).Result;
                }    
                if (_sign.Phone == username || _sign.Email == username)
                {
                    return await  Task.FromResult(_sign);
                }
                else
                {
                    return await Task.FromResult(new Student());
                }    
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new Student());
            }

        }
        public void Dispose()
        {
          //  db.Dispose();
        }
    }
}