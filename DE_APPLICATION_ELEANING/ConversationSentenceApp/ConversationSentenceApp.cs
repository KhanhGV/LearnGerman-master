using DE_APPLICATION_ELEANING.ConversationSentenceApp.Dto;
using DE_APPLICATION_ELEANING.CurrenApp;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace DE_APPLICATION_ELEANING.ConversationSentenceApp
{
    public class ConversationSentenceApp : AsyncCrudAppService
        <ConversationSentenceDto, ConversationSentence, ConversationSentence,
        Guid, List<ConversationSentence>, bool,
        bool, ConversationSentence, bool>, IConversationSentenceApp
    {
        public readonly Entities _Db_Context;
        public ConversationSentenceApp()
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
                var _result = _Db_Context.ConversationSentences.Find(Id);
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

        public override async Task<bool> CreateAsync(ConversationSentence input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.ConversationSentences.Add(input);
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
                var _result = _Db_Context.ConversationSentences.Find(input);

                _Db_Context.ConversationSentences.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public override async Task<ConversationSentence> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.ConversationSentences.Find(input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<ConversationSentence>> GetListAsync(ConversationSentenceDto input)
        {
            Func<ConversationSentence, bool> where = x =>
           ((!string.IsNullOrEmpty(input.ConversationId.ToString())) ? x.ConversationId.ToString().ToLower().Contains(input.ConversationId.ToString().ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status.ToString().ToLower().Contains(input.Status.ToString().ToLower()) : true);
            var _result = _Db_Context.ConversationSentences.Where(where).ToList();
            return await Task.FromResult(_result);
        }
        public override async Task<bool> UpdateAsync(ConversationSentence input)
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
