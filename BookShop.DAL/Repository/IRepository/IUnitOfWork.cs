using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DAL.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get;}
       public ICoverTypeRepository CoverType { get;}

        void Save();
    }
}
