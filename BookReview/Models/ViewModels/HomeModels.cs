using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookReview.Models.TemplateModels;

namespace BookReview.Models.ViewModels
{
    public class HomeIndex
    {
        public List<BookBox> ListBookBox { get; set; }
    }
}