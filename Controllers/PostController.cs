using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class PostController : Controller
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
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44345/");
            client.PostAsJsonAsync<Post>("http://localhost:8081/api/post/", post).ContinueWith((posTask) => posTask.Result.EnsureSuccessStatusCode());

            return RedirectToAction("Index");
            Response.Redirect("~/default.aspx"); // Or whatever your page url



        }

        /*[HttpPost]
        public ActionResult Create(Post post)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44345/");
            client.PostAsJsonAsync<Post>("http://localhost:8081/api/post/", post).ContinueWith((posTask) => posTask.Result.EnsureSuccessStatusCode());



   
                using (var content = new MultipartFormDataContent())
                {
                    byte[] fileBytes = new byte[post.ImageFile.InputStream.Length + 1];
                post.ImageFile.InputStream.Read(fileBytes, 0, fileBytes.Length);
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = post.ImageFile.FileName };
                    content.Add(fileContent);
              
                var result = client.PostAsync("http://localhost:8081/uploadMultiFiles", content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        ViewBag.Message = "Created";
                    }
                    else
                    {
                        ViewBag.Message = "Failed";
                    }
               
            }

            return RedirectToAction("Index");
            //  Response.Redirect("~/default.aspx"); // Or whatever your page url
        }*/

        [HttpGet]
        public ActionResult TrendingPosts()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44345/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8081/api/post/trendingPosts").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result2 = response.Content.ReadAsAsync<IEnumerable<Post>>().Result;
            }
            else
            {
                ViewBag.result2 = " erreur";
            }
            return View();
        }


        [HttpGet]
        public ActionResult GetById(int id)
        {
            string a = "http://localhost:8081/api/post/" + id;

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44345/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8081/api/post/trendingPosts").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Post>>().Result;
            }
            else
            {
                ViewBag.result = " erreur";
            }
            return View();
        }

        [HttpPost]
        public ActionResult UserByUserName(string userName)
        {
            // do whatever you want to do with `userName`

            return View();

        }

        [HttpGet]
        public ActionResult Details(int id)
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
        [HttpPost]
        public ActionResult CommentPost(Post post)
             
        {

            CommentPost comment = new CommentPost();
            comment.date = DateTime.Now.ToString("yyyy-MM-dd");
            comment.text = post.singleComment;
            comment.post = new { id = post.id  };

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Client.PostAsJsonAsync<CommentPost>("http://localhost:8081/api/commentpost/", comment).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            //localhost: 8081 / api / commentpost /


            //var APIResponse = httpClient.PostAsJsonAsync<Product>(baseAddress + " product /",
            //product).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());





            return RedirectToAction("Index");


        }


        public ActionResult Likes(int id)
        {
            string url = "http://localhost:8081/api/commentpost/like/" + id;

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



        public ActionResult Dislikes(int id )
        {
            string url = "http://localhost:8081/api/commentpost/dislike/" + id;

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
        [HttpGet]
        public ActionResult Deletecomment(int id)
        {
            string a = "http://localhost:8081/api/commentpost/" + id;

            using (HttpClient client = new HttpClient())
            {
                string Url = "http://localhost:8081/api/post/";
                var uri = new Uri(Url);
                var response = client.DeleteAsync(a).Result;
            }
            return Redirect(Request.UrlReferrer.ToString());
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
       

    }
}
