using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Models.TemplateModels
{
    public class BookCommentTemplateModel
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string Creater { get; set; }
        public int? CreaterId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public double? Rating { get; set; }
    }

    public class MemberBookCommentTemplateModel : BookCommentTemplateModel
    {
        public string BookName { get; set; }
        public string BookImagePath { get; set; }
    }
}