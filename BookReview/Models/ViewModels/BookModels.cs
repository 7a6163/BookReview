using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookReview.Models.TemplateModels;

namespace BookReview.Models.ViewModels
{
    public class BookModels
    {
    }

    public class BookBookDetail : BookBox
    {
        public BookCommentTemplateModel BookComment { get; set; }
        public List<BookCommentTemplateModel> ListBookComment { get; set; }
    }
}