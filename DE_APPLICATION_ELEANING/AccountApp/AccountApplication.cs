using DE_APPLICATION_ELEANING.CurrenApp;
using System;
using System.Collections.Generic;
using DE_DB_ELEANING.AFModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DE_APPLICATION_ELEANING.StudentApp.Dto;
using System.Data.Entity;

namespace DE_APPLICATION_ELEANING.AccountApp
{
    public  class AccountApplication : AsyncCrudAppService
        <StudenSearchDto, Account, Account,
        Guid, List<Account>, bool,
        bool, Account, bool>, IAccountApplication<Account>
    {
        public readonly Entities _Db_Context;

        public AccountApplication()
        {
            if (_Db_Context == null)
            {
                _Db_Context = new Entities();
            }
        }
        public override async Task<bool> CreateAsync(Account input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.Accounts.Add(input);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public override Task<bool> DeleteAsync(Guid input)
        {
            throw new NotImplementedException();
        }

        public override async Task<Account> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.Accounts.Find(input);
            return await Task.FromResult(_result);
        }

        public async Task<Account> GetByUserName(string username)
        {
            var _result = _Db_Context.Accounts.Where(a=>a.Username==username).FirstOrDefault();
            return await Task.FromResult(_result);
        }

        public override Task<List<Account>> GetListAsync(StudenSearchDto input)
        {
            throw new NotImplementedException();
        }

        public  Photo GetPhoByUserName(string username)
        {
            try
            {
                var account = _Db_Context.AspNetUsers.FirstOrDefault(a => a.UserName == username);
                var photo = _Db_Context.Photos.FirstOrDefault(a => a.AppUserId == account.Id);
                if(photo== null)
                {
                    Photo create = new Photo();
                    create.AppUserId = account.Id;
                    create.IsMain = true;
                    _Db_Context.Photos.Add(create);
                    _Db_Context.SaveChanges();
                    var photos = _Db_Context.Photos.FirstOrDefault(a => a.AppUserId == account.Id);
                    return photos;
                }
                else
                {
                    return photo;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override async Task<bool> UpdateAsync(Account input)
        {
            try
            {
                _Db_Context.Entry(input).State = EntityState.Modified;
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public bool UpdatePhoto(string usernmae,string url)
        {

            try
            {
                var _UserChat = _Db_Context.AspNetUsers.FirstOrDefault(a => a.UserName == usernmae);
                var _phoTo = _Db_Context.Photos.FirstOrDefault(a => a.AppUserId == _UserChat.Id);
                _phoTo.Url = url;
                _Db_Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
