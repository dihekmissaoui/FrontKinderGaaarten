using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DetailPostController : Controller
    {
        // GET: DetailPost
        public ActionResult Index(int id)
        {
            string a = "http://localhost:8081/api/post/" + id;
            Post p = new Post();
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44345/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync(a).Result;

            if (response.IsSuccessStatusCode)
            {
                p = response.Content.ReadAsAsync<Post>().Result;
            }

            return View(p);
        }
    }
}