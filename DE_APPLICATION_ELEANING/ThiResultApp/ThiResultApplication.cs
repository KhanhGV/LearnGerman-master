using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.ThiResultApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiResultApp
{
    public class ThiResultApplication : AsyncCrudAppService
       <SearchThiResult, ThiResult, ThiResult,
       Guid, List<ThiResult>, bool,
       bool, ThiResult, bool>, IThiResultApplication
    {
        public readonly Entities _Db_Context;

        public ThiResultApplication()
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
                var _result = _Db_Context.ThiResults.Find(Id);
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

        public override async Task<bool> CreateAsync(ThiResult input)
        {
            try
            {
                input.ID = Guid.NewGuid();
                _Db_Context.ThiResults.Add(input);
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
                var _result = _Db_Context.ThiResults.Find(id);
                _Db_Context.ThiResults.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
        //public  async Task<ThiResult> CheckExistResult(SearchResultApi input)
        //{

        //    Func<ThiResult, bool> where = x => ( (!string.IsNullOrEmpty(input.DeThiID.ToString()) ? x.DeThiID == input.DeThiID : false)
        //    && (!string.IsNullOrEmpty(input.QuestionID.ToString()) ? x.QuestionID == input.QuestionID : false)
        //     && (!string.IsNullOrEmpty(input.StudentID.Value.ToString()) ? x.StudentID == input.StudentID : false)
        //    );

        //    var _result = await  _Db_Context.ThiResults.ToListAsync();
        //    return await Task.FromResult(_result);
        //}

        public override async Task<ThiResult> GetbyIdAsync(Guid id)
        {
            var _result = _Db_Context.ThiResults.Find(id);
            return await Task.FromResult(_result);
        }

        public override async Task<List<ThiResult>> GetListAsync(SearchThiResult input)
        {
            if (input.Type == "Check")
            {
                Func<ThiResult, bool> where = x => ((!string.IsNullOrEmpty(input.DeThiID.ToString()) ? x.DeThiID == input.DeThiID : false)
            && (!string.IsNullOrEmpty(input.QuestionID.ToString()) &&  x.ThiQuestion.IsDelete != true  ? x.QuestionID == input.QuestionID : false)
             && (!string.IsNullOrEmpty(input.StudentID.Value.ToString()) ? x.StudentID == input.StudentID : false)
             && (!string.IsNullOrEmpty(input.StudentID.Value.ToString()) ? x.AnswerID == input.AnswerID : false)
            );

                var _result =  _Db_Context.ThiResults.Where(where).ToList();
                return await Task.FromResult(_result);
            }
            else
            {


                if (input.DeThiID != null && input.StudentID != null)
                {
                    Func<ThiResult, bool> where = x => (x.DeThiID == input.DeThiID && x.StudentID == input.StudentID);
                    var _result = _Db_Context.ThiResults.Where(where).ToList();
                    return await Task.FromResult(_result);
                }else if (input.DeThiID != null && input.StudentID == null)
                {
                    Func<ThiResult, bool> where = x => (x.DeThiID == input.DeThiID);
                    var _result = _Db_Context.ThiResults.Where(where).ToList();
                    return await Task.FromResult(_result);
                }else if (input.DeThiID == null && input.StudentID != null)
                {
                    Func<ThiResult, bool> where = x => (x.StudentID == input.StudentID);
                    var _result = _Db_Context.ThiResults.Where(where).ToList();
                    return await Task.FromResult(_result);
                }
                else
                {
                    var _result = _Db_Context.ThiResults.ToList();
                    return await Task.FromResult(_result);
                }
                //Func<ThiResult, bool> where = x => (
                //(!string.IsNullOrEmpty(x.DeThiID.ToString()) ? x.DeThiID == input.DeThiID : true)
                //&&
                //(!string.IsNullOrEmpty(x.StudentID.ToString()) ? x.StudentID == input.StudentID : true)
                //);
                //var _result = _Db_Context.ThiResults.Where(where).ToList();
                //return await Task.FromResult(_result);
            }



        }

        public override async Task<bool> UpdateAsync(ThiResult input)
        {

            try
            {
                var local = _Db_Context.Set<ThiResult>()
                         .Local
                         .FirstOrDefault(f => f.ID == input.ID);
                if (local != null)
                {
                    _Db_Context.Entry(local).State = EntityState.Detached;
                }
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
