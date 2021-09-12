using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.DeThiApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.DeThiApp
{
    public class DeThiApplication : AsyncCrudAppService
       <SearchDeThi, ThiDeThi, ThiDeThi,
       Guid, List<ThiDeThi>, bool,
       bool, ThiDeThi, bool>, IDeThiApplication
    {
        public readonly Entities _Db_Context;

        public DeThiApplication()
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
                var _result = _Db_Context.ThiDeThis.Find(Id);
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

        public override async Task<bool> CreateAsync(ThiDeThi input)
        {
            try
            {
                input.ID = Guid.NewGuid();
                _Db_Context.ThiDeThis.Add(input);
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
                var _result = _Db_Context.ThiDeThis.Find(id);
                _Db_Context.ThiDeThis.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch
            {
                var _result = _Db_Context.ThiDeThis.Find(id);
                _result.IsDelete = true;
                await this.UpdateAsync(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
        }

        public override async Task<ThiDeThi> GetbyIdAsync(Guid id)
        {
            var _result = _Db_Context.ThiDeThis.Find(id);
            return await Task.FromResult(_result);
        }

        public override async Task<List<ThiDeThi>> GetListAsync(SearchDeThi input)
        {
            if(input.Type == "luat")
            {
                var _result = await _Db_Context.ThiDeThis.ToListAsync();
                return await Task.FromResult(_result);
            }
            else
            {
                Func<ThiDeThi, bool> where = x => (!string.IsNullOrEmpty(input.NameVi) ? x.Name == input.NameVi : true)
            && (!string.IsNullOrEmpty(input.NameDe) ? x.NameDe == input.NameDe : true)
            && (input.Status != null ? x.Status == input.Status : true)
            && (x.IsDelete != true);
                var _result = _Db_Context.ThiDeThis.Where(where).ToList();
                return await Task.FromResult(_result);
            }
            
        }

        public override async Task<bool> UpdateAsync(ThiDeThi input)
        {

            try
            {
                var local = _Db_Context.Set<ThiDeThi>()
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
