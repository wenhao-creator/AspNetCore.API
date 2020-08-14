using System.Collections.Generic;
using System.Data;
using AspNetCore.Common.Enums;
using AspNetCore.IRepository.Base;
using Dapper;
using Dapper.Contrib.Extensions;
using Z.Dapper.Plus;

namespace AspNetCore.Repository.Base
{
    public class BaseRepository : IBaseRepository
    {
        protected IDbConnection connection;
        protected ICustomConnectionFactory factory;

        public BaseRepository(ICustomConnectionFactory factory)
        {
            this.factory = factory;
        }

        #region 增删改查

        #region Insert

        public bool Insert<T>(T t) where T : class
        {
            Write();
            return connection.Insert<T>(t) > 0;
        }

        public bool Insert(string sql, DynamicParameters parameters)
        {
            Write();
            return connection.Execute(sql, parameters) > 0;
        }

        public IEnumerable<T> BulkInsert<T>(IEnumerable<T> ts)
        {
            Write();
            DapperPlusActionSet<T> result = connection.BulkInsert<T>(ts);
            return result.Current;
        }

        #endregion Insert

        #region Delete

        public bool Delete<T>(int Id) where T : class
        {
            Write();
            T t = connection.Get<T>(Id);
            return connection.Delete<T>(t);
        }

        public bool Delete(string sql, DynamicParameters parameters)
        {
            Write();
            return connection.Execute(sql, parameters) > 0;
        }

        public void BulkDelete<T>(IEnumerable<T> ts)
        {
            Write();
            connection.BulkDelete<T>(ts);
        }

        #endregion Delete

        #region Update

        public bool Update<T>(T t) where T : class
        {
            Write();
            return connection.Update<T>(t);
        }

        public T BulkUpdate<T>(IEnumerable<T> ts)
        {
            Write();
            DapperPlusActionSet<T> result = connection.BulkUpdate<T>(ts);
            return result.CurrentItem;
        }

        #endregion Update

        #region Query

        public IEnumerable<T> Query<T>(string sql)
        {
            Read();
            return connection.Query<T>(sql);
        }

        public T Get<T>(int Id) where T : class
        {
            Read();
            return connection.Get<T>(Id);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            Read();
            return connection.GetAll<T>();
        }

        #endregion Query

        #endregion 增删改查

        private void Write()
        {
            connection = factory.GetDbConnection(WriteRead.Write);
        }

        private void Read()
        {
            connection = factory.GetDbConnection(WriteRead.Read);
        }
    }
}