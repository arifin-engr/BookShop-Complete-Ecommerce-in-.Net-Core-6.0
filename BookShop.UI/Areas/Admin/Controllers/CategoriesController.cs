using BookShop.UI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookShop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        Uri baseUrl = new Uri("http://localhost:5204/api");
        HttpClient _client;
        public CategoriesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
        }
        public IActionResult Index()
        {
            List<CategoryVM> itemVm = new List<CategoryVM>();
            HttpResponseMessage response = _client.GetAsync(baseUrl + "/Categories").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                itemVm = JsonConvert.DeserializeObject<List<CategoryVM>>(data);
            }

            return View(itemVm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryVM obj)
        {

            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage respone = _client.PostAsync(baseUrl + "/Categories/", content).Result;
                if (respone.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Successfully Created";
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
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
        public IActionResult Edit(CategoryVM obj)
        {

            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage respone = _client.PutAsync(baseUrl + "/Categories/", content).Result;
                if (respone.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Successfully Updated";
                    return RedirectToAction("Index");
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
    }
}
