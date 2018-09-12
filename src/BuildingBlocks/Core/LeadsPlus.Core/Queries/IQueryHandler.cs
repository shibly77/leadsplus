namespace LeadsPlus.Core.Query
{
    using System.Threading.Tasks;

    public interface IQueryHandler<Query, Result>
    {
         Task<Result> Handle(Query query);
    }
}
