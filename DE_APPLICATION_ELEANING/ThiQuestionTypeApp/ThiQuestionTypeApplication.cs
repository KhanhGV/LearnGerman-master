using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.ThiQuestionTypeApp;
using DE_APPLICATION_ELEANING.ThiQuestionTypeApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiQuestionTypeTypeApp
{    
   public class ThiQuestionTypeTypeApplication : AsyncCrudAppService
       <SearchThiQuestionType, ThiQuestionType, ThiQuestionType,
       Guid, List<ThiQuestionType>, bool,
       bool, ThiQuestionType, bool>, IThiQuestionTypeApplication
    {
        public readonly Entities _Db_Context;

        public ThiQuestionTypeTypeApplication()
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
                var _result = _Db_Context.ThiQuestionTypes.Find(Id);
                //   _result.Status = status;
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

        public override async Task<bool> CreateAsync(ThiQuestionType input)
        {
            try
            {
                input.ID = Guid.NewGuid();
                _Db_Context.ThiQuestionTypes.Add(input);
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
                var _result = _Db_Context.ThiQuestionTypes.Find(id);
                _Db_Context.ThiQuestionTypes.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public override async Task<ThiQuestionType> GetbyIdAsync(Guid id)
        {
            var _result = _Db_Context.ThiQuestionTypes.Find(id);
            return await Task.FromResult(_result);
        }

        public override async Task<List<ThiQuestionType>> GetListAsync(SearchThiQuestionType input)
        {
            //Func<ThiQuestionType, bool> where = x => (x.DeThiID == input.ID);

            var _result = _Db_Context.ThiQuestionTypes.ToList();
            return await Task.FromResult(_result);
        }

        public override async Task<bool> UpdateAsync(ThiQuestionType input)
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
