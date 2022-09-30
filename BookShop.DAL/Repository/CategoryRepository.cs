using BookShop.DAL.Data;
using BookShop.DAL.Repository.IRepository;
using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DAL.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
       private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Category entity)
        {
            _db.Categories.Update(entity);
        }
    }
}
