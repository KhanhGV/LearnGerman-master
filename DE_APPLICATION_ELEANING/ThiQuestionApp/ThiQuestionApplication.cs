using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.ThiQuestionApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiQuestionApp
{
     public class ThiQuestionApplication : AsyncCrudAppService
       <SearchThiQuestion, ThiQuestion, ThiQuestion,
       Guid, List<ThiQuestion>, bool,
       bool, ThiQuestion, bool>, IThiQuestionApplication
    {
        public readonly Entities _Db_Context;

        public ThiQuestionApplication()
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
                var _result = _Db_Context.ThiQuestions.Find(Id);
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
        public override async Task<bool> CreateAsync(ThiQuestion input)
        {
            try
            {
                //input.ID = Guid.NewGuid();
                _Db_Context.ThiQuestions.Add(input);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch(Exception ex)
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
                var _result = _Db_Context.ThiQuestions.Find(id);
                _Db_Context.ThiQuestions.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
                var _result = _Db_Context.ThiQuestions.Find(id);
                _result.IsDelete = true;
                 var del = await this.UpdateAsync(_result);
                return await Task.FromResult(del);
            }
        }
        public override async Task<ThiQuestion> GetbyIdAsync(Guid id)
        {
            var _result = _Db_Context.ThiQuestions.Find(id);
            return await Task.FromResult(_result);
        }
        public async Task<List<ThiQuestion>> GetListAsync(SearchThiQuestionApi input)
        {
            
                Func<ThiQuestion, bool> where = x => ((!string.IsNullOrEmpty(input.BaiTapThiID.ToString()) ? x.BaiTapThiID == input.BaiTapThiID : false)                
                && (x.IsDelete != true));
                var _result = _Db_Context.ThiQuestions.Where(where).ToList();
                return await Task.FromResult(_result);
           
        }
        public override async Task<List<ThiQuestion>> GetListAsync(SearchThiQuestion input)
        {           
            if(input.TypeSearch == "TypeSubject")
            {
                Func<ThiQuestion, bool> where = x => (x.BaiTapThiID == input.BaiTapThiID) && (x.IsDelete != true);

                var _result = _Db_Context.ThiQuestions.Where(where).ToList();
                return await Task.FromResult(_result);
            }
            else
            {
                Func<ThiQuestion, bool> where = x => (x.BaiTapThiID == input.BaiTapThiID) && (x.IsDelete != true);

                var _result = _Db_Context.ThiQuestions.Where(where).ToList();
                return await Task.FromResult(_result);
            }
        }
        public override async Task<bool> UpdateAsync(ThiQuestion input)
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
