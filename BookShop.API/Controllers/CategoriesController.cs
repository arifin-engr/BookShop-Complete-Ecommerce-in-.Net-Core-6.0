using BookShop.DAL.Repository.IRepository;
using BookShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
      
        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Category.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Category GetCategoryById(int? id)
        {
            
            Category obj = _unitOfWork.Category.GetFirstOrDeFault(x => x.Id == id);
            return obj;
        }
        [HttpPost]
     
        public void Create(Category obj)
        {
            HttpResponseMessage response;
           _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
        }

        [HttpPut]
     
        public void Update(Category obj)
        {
           
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
           
           
        }
        [HttpDelete]
        [Route("{id}")]
        public void Remove(int id)
        {
            Category obj = _unitOfWork.Category.GetFirstOrDeFault(x => x.Id == id);
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
        }
    }
}
