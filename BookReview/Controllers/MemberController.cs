using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BookReview.Models.TemplateModels;
using BookReview.Models.ViewModels;
using Domain;

namespace BookReview.Controllers
{
    public class MemberController : Controller
    {
        private readonly MainEntities _mainEntities = new MainEntities();
        private readonly BookEntities _bookEntities = new BookEntities();

        //
        // GET: /Member/

        [Authorize]
        public ActionResult Index()
        {
            MemberIndex model = new MemberIndex();

            UserProfile userProfile = _mainEntities.UserProfile.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            model.MemberInfomation = new MemberInfomation() { UserName = userProfile.UserName, Name = userProfile.Name };

            var comments = _bookEntities.Comments.Where(c => c.CreaterId == userProfile.UserId);

            model.ListBookBookComment = new List<MemberBookCommentTemplateModel>();

            Mapper.CreateMap<Comments, MemberBookCommentTemplateModel>();
            foreach (var bookComment in comments)
            {
                var memberBookCommentTemplateModel = new MemberBookCommentTemplateModel();
                Mapper.Map(bookComment, memberBookCommentTemplateModel);

                var book = _bookEntities.Books.Find(bookComment.BookId);
                memberBookCommentTemplateModel.BookName = book.Name;
                memberBookCommentTemplateModel.BookImagePath = book.ImagePath;

                model.ListBookBookComment.Add(memberBookCommentTemplateModel);
            }

            return View(model);
        }

    }
}
