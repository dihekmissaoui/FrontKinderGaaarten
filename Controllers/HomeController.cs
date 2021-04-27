using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Post
        public ActionResult Index(string title)
        {
            string url;
            if (String.IsNullOrEmpty(title))
            {
                url = "http://localhost:8081/api/post/";
            }

            else
            {
                url = "http://localhost:8081/api/post/getPostByTitle/" + title;
            }
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44345/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Post>>().Result;
            }
            else
            {
                ViewBag.result = " erreur";
            }

            HttpResponseMessage response2 = Client.GetAsync("http://localhost:8081/api/post/trendingPosts").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result2 = response2.Content.ReadAsAsync<IEnumerable<Post>>().Result;
            }
            // Response.AddHeader("Refresh", "5");

            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            string a = "http://localhost:8081/api/post/" + id;

            using (HttpClient client = new HttpClient())
            {
                string Url = "http://localhost:8081/api/post/";
                var uri = new Uri(Url);
                var response = client.DeleteAsync(a).Result;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult Edit(int id)
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


        public ActionResult Edit(Post post)
        {

            //   post.date = post.date.ToString("yyyyMMdd");

            string url = "http://localhost:8081/api/post/" + post.id;

            using (HttpClient client = new HttpClient())
            {

                //   var result = client.PutAsync(a, new StringContent(post.v)).Result;
                var json = new JavaScriptSerializer().Serialize(post);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PutAsync(url, content).Result;
            }

            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Likes(int id)
        {
            string url = "http://localhost:8081/api/post/like/" + id;

            using (HttpClient client = new HttpClient())
            {

                //   var result = client.PutAsync(a, new StringContent(post.v)).Result;
                var json = new JavaScriptSerializer().Serialize(id);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PutAsync(url, content).Result;
            }

            return RedirectToAction("Index");
        }



        public ActionResult Dislikes(int id)
        {
            string url = "http://localhost:8081/api/post/dislike/" + id;

            using (HttpClient client = new HttpClient())
            {

                //   var result = client.PutAsync(a, new StringContent(post.v)).Result;
                var json = new JavaScriptSerializer().Serialize(id);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PutAsync(url, content).Result;
            }

            return RedirectToAction("Index");
        }

    }


}
