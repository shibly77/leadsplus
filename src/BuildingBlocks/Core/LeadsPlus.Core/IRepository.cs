namespace LeadsPlus.Core
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<V> where V : IViewModel
    {
        Task AddAsync(V item);
        IMongoCollection<V> Collection { get; }
        Task AddRangeAsync(IEnumerable<V> items);
        Task UpdateOneByIdAsync(string itemId, IEnumerable<Update> updates);
        Task UpdateOneAsync(IEnumerable<Filter> filters, IEnumerable<Update> updates);
        Task UpdateManyByIdAsync(string itemId, IEnumerable<Update> updates);
        Task UpdateManyAsync(IEnumerable<Filter> filters, IEnumerable<Update> updates);
        Task DeleteAsync(V entity, string itemId);
        Task<V> Single(FilterDefinition<V> expression);
        bool Any(Expression<Func<V, bool>> expression);
        int Count(Expression<Func<V, bool>> expression);
        IEnumerable<V> TextSearch(string text, int skipped, int pageSize);
        long TextSearchCount(string text);
        IEnumerable<V> GetPaged(Expression<Func<V, bool>> filter, int skip, int take);
        IEnumerable<V> GetPagedDescending(Expression<Func<V, bool>> filter, int skip, int take, Expression<Func<V, object>> orderBy);
        IEnumerable<TField> GetDistinct<TField>(Expression<Func<V, TField>> field, Expression<Func<V, bool>> filter);
        IEnumerable<Model> GetByRegulerExpression<Model>(string fieldName, string value);
        IEnumerable<M3> GetRatingAverage<M1, M2, M3>(string reviewrId, DateTime fromDate, DateTime toDate) where M1 : M3 where M2 : M3;
        IEnumerable<M3> GetRatingAverageMonthly<M1, M2, M3>(string reviewrId, DateTime fromDate, DateTime toDate) where M1 : M3 where M2 : M3;

        Task ReplaceOneAsync(string itemId, V document);
    }

    public class UpdateOperations
    {
        public const string Set = "$set";
        public const string IncrementOrDecrementNumber = "$inc";
    }
    public class FilterOperators
    {
        public const string Equal = "$eq";
        public const string NotEquals = "$ne";
    }
    public class Update
    {
        public object Value { get; set; }
        public string FieldName { get; set; }
        public string UpdateOperation { get; set; }
    }

    public class Filter
    {
        public object Value { get; set; }
        public string FieldName { get; set; }
        public string FilterOperator { get; set; }
    }
}

