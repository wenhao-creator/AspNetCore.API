using AspNetCore.IService;
using AspNetCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.API.Controllers
{
    /// <summary>
    ///  Company （公司）控制器
    /// </summary>
    public class CompanyController : BaseController
    {
        private ICompanyService companyService;

        public CompanyController(ICompanyService company)
        {
            companyService = company;
        }

        /// <summary>
        /// 获取 company 分页
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCompanyPage")]
        public IActionResult GetCompanyPage(Page page) => Ok(companyService.GetPage<Company>(page));
    }
}