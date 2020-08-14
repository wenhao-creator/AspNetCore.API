using AspNetCore.IRepository;
using AspNetCore.IService;
using AspNetCore.Model;

namespace AspNetCore.Service
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository company;

        public CompanyService(ICompanyRepository companyRepository)
        {
            company = companyRepository;
        }

        /// <summary>
        /// Company 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <returns></returns>
        public TPageResult<T> GetPage<T>(Page page) where T : class
        {
            return company.GetPage<T>(page);
        }
    }
}