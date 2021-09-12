using DE_APPLICATION_ELEANING.CurrenApp;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace DE_APPLICATION_ELEANING.MediaApp
{
    public class MediaApplication : AsyncCrudAppService
        <object, Medium, Medium,
        Guid, List<Medium>, bool,
        bool, Medium, bool>, IMediaApplication
    {
        public readonly Entities _Db_Context;

        public MediaApplication()
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
                var _result = _Db_Context.Media.Find(Id);
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

        public override async Task<bool> CreateAsync(Medium input)
        {
            try
            {
                _Db_Context.Media.Add(input);
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
                var _result = _Db_Context.Media.Find(input);
                _Db_Context.Media.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public override async Task<Medium> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.Media.Single(c=>c.Id == input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<Medium>> GetListAsync(object input)
        {
            var _result = _Db_Context.Media.ToList();
            return await Task.FromResult(_result);
        }

        public override async Task<bool> UpdateAsync(Medium input)
        {
            try
            {
                input.Words1 = null;
                input.Words = null;
                input.Questions = null;
                input.Questions1 = null;
                _Db_Context.Entry(input).State = EntityState.Modified;
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch(Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

    }
}
