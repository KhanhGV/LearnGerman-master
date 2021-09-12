using System.Threading.Tasks;
namespace DE_APPLICATION_ELEANING.CurrenApp
{
    public abstract class AsyncCrudAppService<TList,TCreat, TUpdate, Tkey, RList, RCreate, RUpdate,RDetail,RDelete>
    {
        public abstract Task<RList> GetListAsync(TList input);
        public abstract Task<RCreate> CreateAsync(TCreat input);

        public abstract Task<RUpdate> UpdateAsync(TUpdate input);
       

        public abstract Task<RDetail> GetbyIdAsync(Tkey input);

        public abstract Task<RDelete> DeleteAsync(Tkey input);

    }
}
