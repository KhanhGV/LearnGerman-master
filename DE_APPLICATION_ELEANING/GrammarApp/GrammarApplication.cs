using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.GrammarApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace DE_APPLICATION_ELEANING.GrammarApp
{
    public class GrammarApplication : AsyncCrudAppService
        <SearchGrammarDto, Grammar_Theory, Grammar_Theory,
        Guid, List<Grammar_Theory>, bool,
        bool, Grammar_Theory, bool>, IGrammarApplication
    {
        public readonly Entities _Db_Context;

        public GrammarApplication()
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
                var _result = _Db_Context.Grammar_Theory.Find(Id);
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

        public override async Task<bool> CreateAsync(Grammar_Theory input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.Grammar_Theory.Add(input);
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
                var _result = _Db_Context.Grammar_Theory.Find(input);
                _Db_Context.Grammar_Theory.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public override async Task<Grammar_Theory> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.Grammar_Theory.Find(input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<Grammar_Theory>> GetListAsync(SearchGrammarDto input)
        {
            Func<Grammar_Theory, bool> where = x =>
           ((!string.IsNullOrEmpty(input.NameDe)) ? x.NameDe.ToLower().Contains(input.NameDe.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.NameVi)) ? x.NamVi.ToLower().Contains(input.NameVi.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true)
           && ((!string.IsNullOrEmpty(input.SubjectId.ToString())) ? x.SubjectId.ToString().ToLower().Contains(input.SubjectId.ToString().ToLower()) : true);
            var _result = _Db_Context.Grammar_Theory.Where(where).ToList();
            return await Task.FromResult(_result);
        }


        public override async Task<bool> UpdateAsync(Grammar_Theory input)
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
