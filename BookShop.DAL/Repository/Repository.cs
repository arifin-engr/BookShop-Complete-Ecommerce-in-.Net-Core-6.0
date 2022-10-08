using BookShop.DAL.Data;
using BookShop.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet=_db.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? includeproperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (includeproperties!=null)
            {
                foreach (var inclueprop in includeproperties.Split(new char[]{ ','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query=  query.Include(inclueprop);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDeFault(Expression<Func<T, bool>> filter, string? includeproperties = null)
        {
           IQueryable<T> query = _dbSet;
            if (includeproperties != null)
            {
                foreach (var inclueprop in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclueprop);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
          _dbSet.RemoveRange(entities);
        }
    }
}
