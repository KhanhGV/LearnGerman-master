using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.WordApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace DE_APPLICATION_ELEANING.WordApp
{
    public class WordApplication : AsyncCrudAppService
        <SearchWorDto, Word, Word,
        Guid, List<Word>, bool,
        bool, Word, bool>, IWordApplication
    {
        public readonly Entities _Db_Context;

        public WordApplication()
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

        public override async Task<bool> CreateAsync(Word input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.Words.Add(input);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
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

        public override async Task<bool> DeleteAsync(Guid input)
        {
            try
            {
                var _result = _Db_Context.Words.Find(input);
                _Db_Context.Words.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                var _result = _Db_Context.Words.Find(input);
                _result.IsDelete = true;
                await this.UpdateAsync(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
        }

        public override async Task<Word> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.Words.Find(input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<Word>> GetListAsync(SearchWorDto input)
        {
            Func<Word, bool> where = x =>
           ((!string.IsNullOrEmpty(input.NameDe)) ? x.WorDe.ToLower().Contains(input.NameDe.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.NameVi)) ? x.WordVi.ToLower().Contains(input.NameVi.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true)
           && (x.IsDelete != true)
           && ((!string.IsNullOrEmpty(input.SubjectId.ToString())) ? x.SubjectId.ToString().ToLower().Contains(input.SubjectId.ToString().ToLower()) : true);
            var _result = _Db_Context.Words.Where(where).ToList();
            return await Task.FromResult(_result);
        }

        public async Task<List<Word>> GetRamDomFailseAnswer(int count, Guid ansTrue)
        {
            List<Word> Grammar_Theory = new List<Word>();
            Random rd = new Random();
            var word = await _Db_Context.Words.ToListAsync();
            var _remove = word.FirstOrDefault(a=>a.Id== ansTrue);
            word.Remove(_remove);
            int CountWord = word.Count;
            for (int i = 0; i < count; i++)
            {
                var _wosdTemp = word[rd.Next(0, CountWord)];
                Grammar_Theory.Add(_wosdTemp);
                word.Remove(_wosdTemp);
                CountWord--;
            }
            return Grammar_Theory;
        }

        public override async Task<bool> UpdateAsync(Word input)
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
