using AbelloLLC.Core.Entities.Common;
using AbelloLLC.DataAccess.Contexts;
using AbelloLLC.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseSectionEntity
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(params string[]? includes)
        {
            var query = _context.Set<T>().OrderByDescending(q=>q.CreatedAt).AsQueryable();
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, params string[]? includes)
        {
            var query = _context.Set<T>().OrderByDescending(d=>d.CreatedAt).AsQueryable();
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(expression);

        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[]? includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteSoft(T entity)
        {
            entity.IsDeleted = true;
        }



        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void DeleteList(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void AddList(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);

        }
    }
}
