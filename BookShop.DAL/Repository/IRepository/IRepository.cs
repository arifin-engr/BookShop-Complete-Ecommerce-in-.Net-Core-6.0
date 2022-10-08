using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DAL.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string ? includeproperties=null);
        void Add(T entity);
        T GetFirstOrDeFault(Expression<Func<T, bool>> filter, string? includeproperties = null);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
