using DE_APPLICATION_ELEANING.QuestionTypeApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DE_APPLICATION_ELEANING.CurrenApp;
using System.Data.Entity;
namespace DE_APPLICATION_ELEANING.QuestionTypeApp
{
    public class QuestionTypeApplication : AsyncCrudAppService
        <SearchQuestionTypeDto, QuestionType, QuestionType,
        Guid, List<QuestionType>, bool,
        bool, QuestionType, bool>, IQuestionTypeApplication
    {
        public readonly Entities _Db_Context;

        public QuestionTypeApplication()
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
                var _result = _Db_Context.QuestionTypes.Find(Id);
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

        public override async Task<bool> CreateAsync(QuestionType input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.QuestionTypes.Add(input);
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

        public override async Task<bool> DeleteAsync(Guid input)
        {
            try
            {
                var _result = _Db_Context.QuestionTypes.Find(input);
                _Db_Context.QuestionTypes.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public override async Task<QuestionType> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.QuestionTypes.Find(input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<QuestionType>> GetListAsync(SearchQuestionTypeDto input)
        {
            Func<QuestionType, bool> where = x =>
           ((!string.IsNullOrEmpty(input.NameDe)) ? x.TypeNameDe.ToLower().Contains(input.NameDe.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.NameVi)) ? x.TypeNameVi.ToLower().Contains(input.NameVi.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.SubjectTypeId.ToString())) ? x.SubjectTypeId.ToString().ToLower().Contains(input.SubjectTypeId.ToString().ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true);
            var _result = _Db_Context.QuestionTypes.Where(where).ToList();
            return await Task.FromResult(_result);
        }

        public override async Task<bool> UpdateAsync(QuestionType input)
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
