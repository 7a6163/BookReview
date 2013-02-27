using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BookReview.Models;
using BookReview.Models.TemplateModels;
using BookReview.Models.ViewModels;
using Domain;
using System.Text;

namespace BookReview.Controllers
{
    public class BookController : Controller
    {
        private readonly BookEntities _bookEntities = new BookEntities();
        private readonly MainEntities _mainEntities = new MainEntities();

        //
        // GET: /Book/

        public ActionResult Index(Guid? id)
        {
            return View();
        }

        public ActionResult AddComment(BookCommentTemplateModel bookComment)
        {
            bookComment.CreateDate = DateTime.UtcNow;
            bookComment.Id = Guid.NewGuid();

            var comments = new Comments() { IsEnable = "1" };

            Mapper.CreateMap<BookCommentTemplateModel, Comments>();
            Mapper.Map(bookComment, comments);

            _bookEntities.Comments.Add(comments);
            _bookEntities.SaveChanges();

            return RedirectToAction("BookDetail", new { id = bookComment.BookId });
        }

        public ActionResult BookDetail(Guid id)
        {
            if (id == null) throw new ArgumentNullException("id");

            var book = _bookEntities.Books.Find(id);
            var model = new BookBookDetail();

            Mapper.CreateMap<Books, BookBookDetail>();
            Mapper.Map(book, model);

            var listComments = _bookEntities.Comments.Where(c => c.BookId.Equals(id)).OrderByDescending(c => c.CreateDate).ToList();
            model.ListBookComment = new List<BookCommentTemplateModel>();

            Mapper.CreateMap<Comments, BookCommentTemplateModel>();
            Mapper.Map(listComments, model.ListBookComment);

            // Init book comment
            model.BookComment = new BookCommentTemplateModel() { BookId = id, CreaterId = User.Identity.IsAuthenticated ? ((UserProfile)_mainEntities.UserProfile.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name))).UserId : -1 };

            //BookBookDetail model = new BookBookDetail()
            //    {
            //        Id = book.Id,
            //        Author = book.Author,
            //        Name = book.Name,
            //        PublishDate = (DateTime)book.PublishDate,
            //        Publisher = book.Publisher
            //    };

            return View(model);
        }

        [HttpPost]
        public string SubmitComment(BookCommentTemplateModel bookComment)
        {
            bookComment.CreateDate = DateTime.UtcNow;
            bookComment.Id = Guid.NewGuid();

            var comments = new Comments() { IsEnable = "1" };

            Mapper.CreateMap<BookCommentTemplateModel, Comments>();
            Mapper.Map(bookComment, comments);

            if (User.Identity.IsAuthenticated)
            {
                var userProfile = _mainEntities.UserProfile.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                if (userProfile != null)
                {
                    comments.CreaterId = userProfile.UserId;
                    comments.Creater = string.IsNullOrWhiteSpace(userProfile.Name) ? "No Name" : userProfile.Name;
                }
            }

            _bookEntities.Comments.Add(comments);
            _bookEntities.SaveChanges();

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<dt>{0}</dt>", comments.CreaterId == null ? "None" : comments.Creater));
            // Compute rating width
            float ratingWidth = (float)((bookComment.Rating) * (80 / 5.0));
            sb.Append(
                string.Format(
                    @"<div class=""rateit"" data-rateit-value=""{0}"" data-rateit-ispreset=""true"" data-rateit-readonly=""true""><div class=""rateit-reset"" style=""display: none;""></div><div class=""rateit-range"" style=""width: 80px; height: 16px;""><div class=""rateit-selected rateit-preset"" style=""height: 16px; width: {1}px;""></div><div class=""rateit-hover"" style=""height:16px""></div></div></div>",
                    bookComment.Rating, ratingWidth));
            sb.Append(string.Format("<dd>{0}{1}</dd>", bookComment.CreateDate, bookComment.Content));

            return sb.ToString();
        }
    }
}
