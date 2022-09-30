using BookShop.UI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookShop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypesController : Controller
    {
        Uri baseUrl = new Uri("http://localhost:5204/api");
        HttpClient _client;
        public CoverTypesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
        }
        public IActionResult Index()
        {
            List<CoverTypeVM> itemVm = new List<CoverTypeVM>();
            HttpResponseMessage response = _client.GetAsync(baseUrl + "/CoverTypes").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                itemVm = JsonConvert.DeserializeObject<List<CoverTypeVM>>(data);
            }

            return View(itemVm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverTypeVM obj)
        {

            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage respone = _client.PostAsync(baseUrl + "/CoverTypes/", content).Result;
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
            CoverTypeVM itemVM = new CoverTypeVM();
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = _client.GetAsync(baseUrl + "/CoverTypes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                itemVM = JsonConvert.DeserializeObject<CoverTypeVM>(data);
                return View(itemVM);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(CoverTypeVM obj)
        {

            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage respone = _client.PutAsync(baseUrl + "/CoverTypes/", content).Result;
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
            CoverTypeVM itemVM = new CoverTypeVM();
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = _client.GetAsync(baseUrl + "/CoverTypes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                itemVM = JsonConvert.DeserializeObject<CoverTypeVM>(data);
                return View(itemVM);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {

            if (ModelState.IsValid)
            {


                HttpResponseMessage respone = _client.DeleteAsync(baseUrl + "/CoverTypes/" + id).Result;
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
