using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApplication3.Controllers
{
    public class CommentPostController : Controller
    {
        // GET: CommentPost
        public ActionResult Index()
        {
            
            return View();
        }

       
    }
}
