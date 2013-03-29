using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookReview.Models.TemplateModels;

namespace BookReview.Models.ViewModels
{
    public class HomeIndex
    {
        public List<BookBox> ListMainBooks { get; set; }
        public List<BookSmallBox> ListPopularBooks { get; set; }
        public List<BookSmallBox> ListArticleBooks { get; set; }
        public List<BookSmallBox> ListNonarticleBooks { get; set; }
    }
}