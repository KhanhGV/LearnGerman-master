using DE_APPLICATION_ELEANING.AswerApp.Dto;
using DE_APPLICATION_ELEANING.CurrenApp;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.AswerApp
{
    public class AswerApplication : AsyncCrudAppService
        <SearchAswerDto, Answer, Answer,
        Guid, List<Answer>, bool,
        bool, Answer, bool>, IAswerApplication
    {
        public readonly Entities _Db_Context;

        public AswerApplication()
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
                var _result = _Db_Context.Answers.Find(Id);
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

        public override async Task<bool> CreateAsync(Answer input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.Answers.Add(input);
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
                var _result = _Db_Context.Answers.Find(id);
                _Db_Context.Answers.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
                var _result = _Db_Context.Answers.Find(id);
                _result.IsDelete = true;
                var del = await this.UpdateAsync(_result);
                return await Task.FromResult(del);
            }
        }

        public override async Task<Answer> GetbyIdAsync(Guid id)
        {
            var _result = _Db_Context.Answers.Find(id);
            return await Task.FromResult(_result);
        }
        public override async Task<List<Answer>> GetListAsync(SearchAswerDto input)
        {
            Func<Answer, bool> where = x =>
            ((!string.IsNullOrEmpty(input.QuestionId.ToString())) ? x.QuestionId.ToString().ToLower().Contains(input.QuestionId.ToString().ToLower()) : true)
            && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.IsTrue == input.Status : true)
            &&  (x.IsDelete != true);
            var _result = _Db_Context.Answers.Where(where).ToList();
            return await Task.FromResult(_result);
        }

        public override async Task<bool> UpdateAsync(Answer input)
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
