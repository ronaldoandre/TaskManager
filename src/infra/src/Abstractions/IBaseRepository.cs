namespace TaskManager.Infra.Abstractions.Repository;

public interface IBaseRepository<T>
    where T : BaseEntity
{
    Task<T> GetById(int id, params Expression<Func<T, object>>[] includes);

    Task<List<T>> Get(Expression<Func<T, bool>> predicate);

    Task<List<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);

    Task<T> Insert(T item);

    Task<IList<T>> InsertRange(IList<T> items);

    Task<T> Update(T item);

    Task<bool> Delete(T item);
}
