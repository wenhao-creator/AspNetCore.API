using AspNetCore.Model;

namespace AspNetCore.IRepository
{
    public interface ICompanyRepository
    {
        public TPageResult<T> GetPage<T>(Page page) where T : class;
    }
}