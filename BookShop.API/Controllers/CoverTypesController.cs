using BookShop.DAL.Repository.IRepository;
using BookShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverTypesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CoverTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
      
        public IEnumerable<CoverType> GetCoverTypes()
        {
            return _unitOfWork.CoverType.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public CoverType GetCoverTypesById(int? id)
        {

            CoverType obj = _unitOfWork.CoverType.GetFirstOrDeFault(x => x.Id == id);
            return obj;
        }
        [HttpPost]
     
        public void Create(CoverType obj)
        {
            HttpResponseMessage response;
           _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
        }

        [HttpPut]
     
        public void Update(CoverType obj)
        {
           
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
           
           
        }
        [HttpDelete]
        [Route("{id}")]
        public void Remove(int id)
        {
            CoverType obj = _unitOfWork.CoverType.GetFirstOrDeFault(x => x.Id == id);
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
        }
    }
}
