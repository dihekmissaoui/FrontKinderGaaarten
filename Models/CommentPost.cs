using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CommentPost
    {
        public int id { get; set; }
        public String date { get; set; }
        public string text { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
        public object post { get; set; }
    }
}