using BookShop.DAL.Repository.IRepository;
using BookShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
       

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        [HttpGet]
      
        public IEnumerable<Product> GetProducts()
        {
            return _unitOfWork.Product.GetAll(includeproperties: "Category,CoverType");
        }

        [HttpGet]
        [Route("{id}")]
        public Product GetProductsById(int? id)
        {

            Product obj = _unitOfWork.Product.GetFirstOrDeFault(x => x.Id == id);
            return obj;
        }

        [HttpPost]
        
     
        public void Create(Product obj)
        {
            HttpResponseMessage response;
            
           _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
        }

        [HttpPut]
     
        public void Update(Product obj)
        {
           
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
           
           
        }
        [HttpDelete]
        [Route("{id}")]
        public void Remove(int id)
        {
            Product obj = _unitOfWork.Product.GetFirstOrDeFault(x => x.Id == id);
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
        }
    }
}
