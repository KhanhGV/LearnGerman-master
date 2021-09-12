using DE_APPLICATION_ELEANING.CurrenApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DE_DB_ELEANING.AFModel;
using DE_APPLICATION_ELEANING.StudentApp.Dto;
using System.Data.Entity;
using DE_DB_ELEANING.Model;

namespace DE_APPLICATION_ELEANING.StudentApp
{
    public class StudentApplication : AsyncCrudAppService
        <StudenSearchDto, Student, Student,
        Guid, List<Student>, bool,
        bool, Student, bool>, IStudentApplication
    {
        public readonly Entities _Db_Context;

        public StudentApplication()
        {
            if (_Db_Context == null)
            {
                _Db_Context = new Entities();
            }
        }

        public async Task<int> Appvore(Guid Id, bool status)
        {
            try
            {
                var _result = _Db_Context.Students.Find(Id);
                _result.Status = status;
                _Db_Context.SaveChanges();
                return await Task.FromResult(1);
            }
            catch
            {
                return await Task.FromResult(0);
            }
        }

        public async Task<int> AppvoreListAsync(string[] List, bool status)
        {
            int count = 0;
            try
            {
                for (int i = 1; i < List.Length; i++)
                {
                    var del = await this.Appvore(Guid.Parse(List[i]), status);
                    count += del;
                }
                return await Task.FromResult(count);
            }
            catch
            {
                return await Task.FromResult(count);
            }
        }

        public override async Task<bool> CreateAsync(Student input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                input.Status = null;//Luat them duyet o day
                _Db_Context.Students.Add(input);
                _Db_Context.SaveChanges();
                var acc = _Db_Context.Accounts.Find(input.AccountId);
                AspNetUser userChat = new AspNetUser();
                userChat.DisplayName = input.Name;
                userChat.UserName = acc.Username;
                userChat.DayOfBirth = DateTime.Now;              
                _Db_Context.AspNetUsers.Add(userChat);
                _Db_Context.SaveChanges();
                var chat = _Db_Context.AspNetUsers.FirstOrDefault(a => a.UserName == acc.Username);
                _Db_Context.InsertRoleUser(chat.Id,2);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<int> DeleleListAsync(string[] List)
        {
            int count = 0;
            try
            {
                for (int i = 1; i < List.Length; i++)
                {
                    var del = await this.DeleteAsync(Guid.Parse(List[i]));
                    if (del == true)
                    {
                        count++;
                    }
                }
                return await Task.FromResult(count);
            }
            catch
            {
                return await Task.FromResult(count);
            }
        }

        public async override Task<bool> DeleteAsync(Guid input)
        {
            try
            {
                var _result = _Db_Context.Students.Find(input);
                var acc = _Db_Context.Accounts.Find(_result.AccountId);
                _Db_Context.Students.Remove(_result);
                _Db_Context.SaveChanges();
                _Db_Context.Accounts.Remove(acc);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<Student> GetByEmail(string email)
        {
            var _result = _Db_Context.Students.Where(a => a.Email == email).FirstOrDefault();
            return await Task.FromResult(_result);
        }

        public override async Task<Student> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.Students.Find(input);
            return await Task.FromResult(_result);
        }

        public async Task<Student> GetByPhone(string phone)
        {
            var _result = _Db_Context.Students.Where(a=>a.Phone==phone).FirstOrDefault();
            return await Task.FromResult(_result);
        }

        public override async Task<List<Student>> GetListAsync(StudenSearchDto input)
        {
            Func<Student, bool> where = x =>
             ((!string.IsNullOrEmpty(input.Name)) ? x.Name.ToLower().Contains(input.Name.ToLower()) : true)
            && ((!string.IsNullOrEmpty(input.Phone)) ? x.Phone.ToLower().Contains(input.Phone.ToLower()) : true)
             && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true);
             var _result = _Db_Context.Students.Where(where).ToList();
            return await Task.FromResult(_result);
        }

        public override async Task<bool> UpdateAsync(Student input)
        {
            try
            {
                var acc = _Db_Context.Accounts.Find(input.AccountId);
                var userChat = _Db_Context.AspNetUsers.FirstOrDefault(a => a.UserName == acc.Username);
                userChat.DisplayName = input.Name;
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
    }
}
