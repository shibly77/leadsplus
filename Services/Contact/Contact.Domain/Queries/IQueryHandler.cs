namespace Contact.Projection.Query
{
    using System.Threading.Tasks;

    public interface IQueryHandler<Query, Result>
    {
        Task<Result> Handle(Query query);
    }
}
