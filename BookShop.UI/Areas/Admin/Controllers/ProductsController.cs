using BookShop.Models;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookShop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        Uri baseUrl = new Uri("http://localhost:5204/api");
        HttpClient _client;
        private readonly IWebHostEnvironment _webHost;
        public ProductsController(IWebHostEnvironment webHost)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            //List<Product> itemVm = new List<Product>();
            //HttpResponseMessage response = _client.GetAsync(baseUrl + "/Products").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    string data = response.Content.ReadAsStringAsync().Result;
            //    itemVm = JsonConvert.DeserializeObject<List<Product>>(data);
            //}

            return View();

        }
        public IActionResult Upsert(int?id)
        {

            
            if (id==null|| id==0)
            {
                HttpResponseMessage response;
                // category item for dropdown
                List<CategoryVM> categoryVm = new List<CategoryVM>();
                 response= _client.GetAsync(baseUrl + "/Categories").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    categoryVm = JsonConvert.DeserializeObject<List<CategoryVM>>(data);
                }

                // coverTypes  item for dropdown
                List<CoverTypeVM> coverTypeVM = new List<CoverTypeVM>();
                 response = _client.GetAsync(baseUrl + "/CoverTypes").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    coverTypeVM = JsonConvert.DeserializeObject<List<CoverTypeVM>>(data);
                }



                ProductVM productVM = new()
                {
                    Product = new(),
                    CategoryList = categoryVm.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    CoverTypeList = coverTypeVM.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value=i.Id.ToString()
                    })
                };


                //create product
                return View(productVM);
            }
           // HttpResponseMessage response = _client.GetAsync(baseUrl + "/Categories/" + id).Result;
            else 
            {
                //update 

                //var data = response.Content.ReadAsStringAsync().Result;
                //categoryVM = JsonConvert.DeserializeObject<CategoryVM>(data);
                //return View(categoryVM);
            }
            return View();
        }
       

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
          
            if (ModelState.IsValid)
            {
                var model = new Product();
                model=obj.Product;
                string mmRoot = _webHost.WebRootPath;
               
                  
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        string filePath = Path.Combine(mmRoot, @"images\ProductImages");
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        var fileExtention = file.FileName;
                        if (obj.Product.ImageURL!=null)
                        {
                            string oldPath = Path.Combine(mmRoot, obj.Product.ImageURL.TrimStart('\\'));
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(filePath, fileName + fileExtention), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        model.ImageURL = @"\images\ProductImages\" + fileName + fileExtention;

                    }
                //Create Product
                if (obj.Product.Id == 0)
                {
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage respone = _client.PostAsync(baseUrl + "/Products/", content).Result;
                    if (respone.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Successfully Created";
                        return RedirectToAction("Index");
                    }
                }

                //Update Products
                else
                {
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage respone = _client.PutAsync(baseUrl + "/Products/", content).Result;
                    if (respone.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Successfully Updated";
                        return RedirectToAction("Index");
                    }
                }

            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            CategoryVM categoryVM = new CategoryVM();
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = _client.GetAsync(baseUrl + "/Categories/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                categoryVM = JsonConvert.DeserializeObject<CategoryVM>(data);
                return View(categoryVM);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {

            if (ModelState.IsValid)
            {


                HttpResponseMessage respone = _client.DeleteAsync(baseUrl + "/Categories/" + id).Result;
                if (respone.IsSuccessStatusCode)
                {
                    TempData["Delete"] = "Successfully Deleted";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        //API CALLS
        #region API Calles

        [HttpGet]
        public IActionResult GetAll()
        {
           
            HttpResponseMessage response = _client.GetAsync(baseUrl + "/Products").Result;
            if (response.IsSuccessStatusCode)
            {
                
                string data = response.Content.ReadAsStringAsync().Result;
                
               var  itemVm = JsonConvert.DeserializeObject<List<Product>>(data);
                return Json(new { data= itemVm });
            }
            return View();
           
        }
        #endregion
    }
}
