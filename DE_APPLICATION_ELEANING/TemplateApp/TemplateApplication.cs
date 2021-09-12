using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.TemplateApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.TemplateApp
{
    public class TemplateApplication : AsyncCrudAppService
        <TemplateSearchDto, Template, Template,
        Guid, List<Template>, bool,
        bool, Template, bool>, ITemplateApplication
    {
        public readonly Entities _Db_Context;

        public TemplateApplication()
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
                var _result = _Db_Context.Templates.Find(Id);
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
        public override async Task<bool> CreateAsync(Template input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.Templates.Add(input);
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
                var _result = _Db_Context.Templates.Find(input);
                _Db_Context.Templates.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public override async Task<Template> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.Templates.Find(input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<Template>> GetListAsync(TemplateSearchDto input)
        {
            Func<Template, bool> where = x =>
           ((!string.IsNullOrEmpty(input.NameDe)) ? x.NoteDe.ToLower().Contains(input.NameDe.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.NameVi)) ? x.NoteVi.ToLower().Contains(input.NameVi.ToLower()) : true)
           && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true)
           && ((!string.IsNullOrEmpty(input.SubjectId.ToString())) ? x.SubjectId.ToString().ToLower().Contains(input.SubjectId.ToString().ToLower()) : true);
            var _result = _Db_Context.Templates.Where(where).ToList();
            return await Task.FromResult(_result);
        }


        public override async Task<bool> UpdateAsync(Template input)
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
