using System.Collections.Generic;
using Dapper;

namespace AspNetCore.IRepository.Base
{
    /// <summary>
    /// IBase
    /// </summary>
    public interface IBaseRepository
    {
        #region 基本操作

        #region Insert

        public bool Insert<T>(T t) where T : class;

        public bool Insert(string sql, DynamicParameters parameters);

        public IEnumerable<T> BulkInsert<T>(IEnumerable<T> ts);

        #endregion Insert

        #region Delete

        public bool Delete<T>(int Id) where T : class;

        public bool Delete(string sql, DynamicParameters parameters);

        public void BulkDelete<T>(IEnumerable<T> ts);

        #endregion Delete

        #region Update

        public bool Update<T>(T t) where T : class;

        public T BulkUpdate<T>(IEnumerable<T> ts);

        #endregion Update

        #region Query

        public IEnumerable<T> Query<T>(string sql);

        public T Get<T>(int Id) where T : class;

        public IEnumerable<T> GetAll<T>() where T : class;

        #endregion Query

        #endregion 基本操作
    }
}