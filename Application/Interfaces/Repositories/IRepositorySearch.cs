﻿using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IRepositorySearch<TModel, TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TModel, bool>> predicate);
    }
}
