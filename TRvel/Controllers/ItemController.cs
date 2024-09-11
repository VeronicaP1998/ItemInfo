using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TRvel.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Ajax.Utilities;
using static System.Net.WebRequestMethods;
using System.Web.Http.Results;
using System.Net;

namespace TRvel.Controllers
{
    public class ItemController : Controller
    {
        private string baseurl= "https://localhost:44369";
        public ActionResult Index(ItemCreate itemCreate)
        {
            return View(itemCreate);
        }
        /// <summary>
        /// to save the item
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ItemCreation(tblItem orders)
        {
            try
            {
                    HttpClient client = new HttpClient
                    {
                        BaseAddress = new Uri(baseurl)
                    };
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string json = JsonConvert.SerializeObject(orders);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("/api/ItemApi/ItemCreation", content);
                    return Json(new { success = true, item = orders });
               }
            catch(Exception ex)
            {
                return Json(HttpStatusCode.InternalServerError,ex.Message);
            }
        }

        /// <summary>
        /// to get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetItemByID(int id)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(baseurl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("/api/ItemApi/GetItemByID/ " + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            tblItem items = JsonConvert.DeserializeObject<tblItem>(responseBody);
            return RedirectToAction("Index", "Item", items);
        }
        [HttpGet]
        public async Task<ActionResult> ItemList()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(baseurl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("/api/ItemApi/GetItemList/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<tblItem> items = JsonConvert.DeserializeObject <List<tblItem>>(responseBody);
            return View(items);
        }

        /// <summary>
        /// to delete item by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ItemDelete(int id)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(baseurl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync("/api/ItemApi/DeleteItem/" + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return Json(new { success = true });
        }
    }
}