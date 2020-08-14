using System.Data;
using System.Text;
using AspNetCore.Common.Enums;
using AspNetCore.IRepository;
using AspNetCore.IRepository.Base;
using AspNetCore.Model;
using AspNetCore.Repository.Base;
using Dapper;

namespace AspNetCore.Repository
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        private new IDbConnection connection;
        private new ICustomConnectionFactory factory;

        public CompanyRepository(ICustomConnectionFactory factory) : base(factory)
        {
            this.factory = factory;
        }

        public TPageResult<T> GetPage<T>(Page page) where T : class
        {
            TPageResult<T> result = new TPageResult<T>();
            if(page.PageIndex <= 0)
            {
                page.PageIndex = 1;
                page.PageSize = 20;
            }

            int start = (page.PageIndex - 1) * page.PageSize;
            int end = start + page.PageSize;

            ReadConnection();
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append("SELECT * FROM [dbo].[Company]");
            sqlBuilder.Append(" WHERE 1=1");
            sqlBuilder.Append(" ORDER BY Id");
            sqlBuilder.Append($" OFFSET {start} ROWS FETCH NEXT {end} ROW ONLY;");

            result.DataList = connection.Query<T>(sqlBuilder.ToString());
            result.PageIndex = page.PageIndex++;
            result.PageSize = page.PageSize;

            return result;
        }

        private void ReadConnection()
        {
            connection = factory.GetDbConnection(WriteRead.Read);
        }
    }
}