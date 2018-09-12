namespace LeadsPlus.Core.Query
{
    using System.Threading.Tasks;
    using Autofac;
    using Autofac.Core;

    public interface IQueryExecutor
    {
        Task<Result> Execute<Query, Result>(Query query);
    }

    public class QueryExecutor : IQueryExecutor
    {
        private readonly Container container;
        public QueryExecutor(Container container)
        {
            this.container = container;
        }

        public Task<Result> Execute<Query, Result>(Query query)
        {
            var queryHandler = container.Resolve(typeof(IQueryHandler<Query, Result>)) as IQueryHandler<Query, Result>;

            var response = queryHandler.Handle(query);

            return response;
        }
    }
}
