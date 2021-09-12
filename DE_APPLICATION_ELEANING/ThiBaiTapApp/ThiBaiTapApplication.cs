using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.ThiBaiTapApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiBaiTapApp
{

    public class ThiBaiTapApplication : AsyncCrudAppService
       <SearchThiBaiTap, ThiBaiTapThi, ThiBaiTapThi,
       Guid, List<ThiBaiTapThi>, bool,
       bool, ThiBaiTapThi, bool>, IThiBaiTap
    {
        public readonly Entities _Db_Context;

        public ThiBaiTapApplication()
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
                var _result = _Db_Context.ThiBaiTapThis.Find(Id);
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

        public override async Task<bool> CreateAsync(ThiBaiTapThi input)
        {
            try
            {
                input.ID = Guid.NewGuid();
                _Db_Context.ThiBaiTapThis.Add(input);
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
                var _result = _Db_Context.ThiBaiTapThis.Find(id);
                _Db_Context.ThiBaiTapThis.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
                var _result = _Db_Context.ThiBaiTapThis.Find(id);
                _result.IsDelete = true;
                var del = await this.UpdateAsync(_result);
                return await Task.FromResult(del);
            }
        }

        public override async Task<ThiBaiTapThi> GetbyIdAsync(Guid id)
        {
            var _result = _Db_Context.ThiBaiTapThis.Find(id);
            return await Task.FromResult(_result);
        }

        public override async Task<List<ThiBaiTapThi>> GetListAsync(SearchThiBaiTap input)
        {
            Func<ThiBaiTapThi, bool> where = x => (
            (!string.IsNullOrEmpty(input.DeThiID.ToString()) ? x.DeThiID == input.DeThiID : false) &&
            (!string.IsNullOrEmpty(input.SubjectTypeId.ToString()) ? x.SubjectTypeId == input.SubjectTypeId : false)
            );

            var _result = _Db_Context.ThiBaiTapThis.Where(where).ToList();
            return await Task.FromResult(_result);
        }

        public override async Task<bool> UpdateAsync(ThiBaiTapThi input)
        {

            try
            {
                var local = _Db_Context.Set<ThiBaiTapThi>()
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
