using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.SubjectTypeApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.SubjectTypeApp
{    
    public class ThiSubjectTypeApplication : AsyncCrudAppService
        <SearchSubjectType, ThiTypeSubject, ThiTypeSubject,
        Guid, List<ThiTypeSubject>, bool,
        bool, ThiTypeSubject, bool>, IThiSubjectTypeApplication
    {
        public readonly Entities _Db_Context;

        public ThiSubjectTypeApplication()
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
                var _result = _Db_Context.ThiTypeSubjects.Find(Id);
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

        public override async Task<bool> CreateAsync(ThiTypeSubject input)
        {
            try
            {
                input.ID = Guid.NewGuid();
                _Db_Context.ThiTypeSubjects.Add(input);
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
                var _result = _Db_Context.ThiTypeSubjects.Find(input);
                _Db_Context.ThiTypeSubjects.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public override async Task<ThiTypeSubject> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.ThiTypeSubjects.Find(input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<ThiTypeSubject>> GetListAsync(SearchSubjectType input)
        {
           // Func<ThiTypeSubject, bool> where = x =>
           //((!string.IsNullOrEmpty(input.NameDe)) ? x.TypeNameDe.ToLower().Contains(input.NameDe.ToLower()) : true)
           //&& ((!string.IsNullOrEmpty(input.NameVi)) ? x.TypeNameVi.ToLower().Contains(input.NameVi.ToLower()) : true)
           //&& ((!string.IsNullOrEmpty(input.SubjectTypeId.ToString())) ? x.SubjectTypeId.ToString().ToLower().Contains(input.SubjectTypeId.ToString().ToLower()) : true)
           //&& ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true);
            var _result = _Db_Context.ThiTypeSubjects.ToList();
            return await Task.FromResult(_result);
        }

        //public override async Task<bool> UpdateAsync(SearchSubjectType input)
        //{
        //    try
        //    {
        //        _Db_Context.Entry(input).State = EntityState.Modified;
        //        _Db_Context.SaveChanges();
        //        return await Task.FromResult(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //        return await Task.FromResult(false);
        //    }
        //}

        public override Task<bool> UpdateAsync(ThiTypeSubject input)
        {
            throw new NotImplementedException();
        }
    }
}
