using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Post
    {
        public int id { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string content { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
        public string singleComment { get; set; }

        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public List<CommentPost> comments = new List<CommentPost>();

        public Post()
            {
           
            }
    }
}