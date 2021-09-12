using DE_APPLICATION_ELEANING.CategoryApp.Dto;
using DE_APPLICATION_ELEANING.CurrenApp;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.CategoryApp
{
    public class CategoryAppLication : AsyncCrudAppService
        <SearchCateDto, SubjectType, SubjectType,
        Guid, List<SubjectType>, bool,
        bool, SubjectType, bool>, ICategoryAppLication
    {
        public readonly Entities _Db_Context;
        public CategoryAppLication()
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
                var _result = _Db_Context.SubjectTypes.Find(Id);
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
                    count+=del;
                }
                return await Task.FromResult(count);
            }
            catch
            {
                return await Task.FromResult(count);
            }
        }

        public override async Task<bool> CreateAsync(SubjectType input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.SubjectTypes.Add(input);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
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

        public override async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var _result = _Db_Context.SubjectTypes.Find(id);
                _Db_Context.SubjectTypes.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public override async Task<SubjectType> GetbyIdAsync(Guid id)
        {
            var _result =  _Db_Context.SubjectTypes.Find(id);
            return  await Task.FromResult(_result);
        }

        public override async Task<List<SubjectType>> GetListAsync(SearchCateDto input)
        {
            Func<SubjectType, bool> where = x =>
            ((!string.IsNullOrEmpty(input.NameDe)) ? x.TypeNameDe.ToLower().Contains(input.NameDe.ToLower()) : true)
            &&((!string.IsNullOrEmpty(input.NameVi)) ? x.TypeNameVi.ToLower().Contains(input.NameVi.ToLower()) : true)
            &&((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status ==input.Status : true);

            var _result =  _Db_Context.SubjectTypes.Where(where).ToList();
            return await Task.FromResult(_result);
        }

        public override async Task<bool> UpdateAsync(SubjectType input)
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
    }
}
