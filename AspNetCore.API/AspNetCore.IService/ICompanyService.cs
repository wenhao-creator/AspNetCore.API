using AspNetCore.Model;

namespace AspNetCore.IService
{
    public interface ICompanyService
    {
        public TPageResult<T> GetPage<T>(Page page) where T : class;
    }
}