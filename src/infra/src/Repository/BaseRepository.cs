namespace TaskManager.Infra.Repository;

class BaseRepository<T>(TaskManagerContext context) : IBaseRepository<T>
    where T : BaseEntity
{
    private readonly DbSet<T> dbSet = context.Set<T>();

    public async Task<bool> Delete(T item)
    {
        try
        {
            dbSet.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
    {
        try
        {
            var query = dbSet.AsNoTracking();

            var models = query.Where(predicate);
            return await Task.FromResult(models.ToList());
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual async Task<T> GetById(int id, params Expression<Func<T, object>>[] includes)
    {
        try
        {
            var query = dbSet.AsNoTracking();

            if (includes is not null && includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual async Task<List<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
    {
        try
        {
            var query = dbSet.AsNoTracking();

            if (includes is not null && includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            var models = query.Where(predicate);
            return await Task.FromResult(models.ToList());
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<T> Insert(T item)
    {
        try
        {
            await dbSet.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<T>> InsertRange(IList<T> items)
    {
        try
        {
            await dbSet.AddRangeAsync(items);
            await context.SaveChangesAsync();
            return items;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<T> Update(T item)
    {
        try
        {
            dbSet.Update(item);
            await context.SaveChangesAsync();
            return item;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
