using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookReview.Models.TemplateModels;

namespace BookReview.Models.ViewModels
{
    public class MemberModels
    {

    }

    public class MemberIndex
    {
        public MemberInfomation MemberInfomation { get; set; }
        public List<MemberBookCommentTemplateModel> ListBookBookComment { get; set; }
    }

    public class MemberAddBook { }
}