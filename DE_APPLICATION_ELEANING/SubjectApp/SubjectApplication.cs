﻿using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.SubjectApp.Dto;
using System;
using System.Collections.Generic;
using DE_DB_ELEANING.AFModel;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
namespace DE_APPLICATION_ELEANING.SubjectApp
{
    public class SubjectApplication : AsyncCrudAppService
        <SearchSubjectDto, Subject, Subject,
        Guid, List<Subject>, bool,
        bool, Subject, bool>, ISubjectApplication
    {
        public readonly Entities _Db_Context;

        public SubjectApplication()
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
                var _result = _Db_Context.Subjects.Find(Id);
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

        public override async Task<bool> CreateAsync(Subject input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.Subjects.Add(input);
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
                var _result = _Db_Context.Subjects.Find(input);
                _Db_Context.Subjects.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public override async Task<Subject> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.Subjects.Find(input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<Subject>> GetListAsync(SearchSubjectDto input)
        {
            Func<Subject, bool> where = x =>
           ((!string.IsNullOrEmpty(input.NameDe)) ? x.NameDe.ToLower().Contains(input.NameDe.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.NameVi)) ? x.NameVi.ToLower().Contains(input.NameVi.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true);
            //&& ((!string.IsNullOrEmpty(input.Type.ToString())) ? x.SubjectTypeId.ToString().ToLower().Contains(input.Type.ToString().ToLower()) : true);
            Func<Subject, bool> parentwhere = x => ((!string.IsNullOrEmpty(input.Type.ToString())) ? x.SubjectTypeId.ToString().ToLower().Contains(input.Type.ToString().ToLower()) : true);
            //var _result = _Db_Context.Subjects.Where(where).Where(parentwhere).ToList();
            try
            {
                var _result = _Db_Context.Subjects.Where(where).Where(parentwhere).ToList();
                return await Task.FromResult(_result);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public override async Task<bool> UpdateAsync(Subject input)
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
