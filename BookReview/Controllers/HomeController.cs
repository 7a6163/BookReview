using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain;
using BookReview.Models.TemplateModels;
using BookReview.Models.ViewModels;

namespace BookReview.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookEntities _bookEntities = new BookEntities();

        public ActionResult Index()
        {
            //for(int i=0;i<20;i++)
            //{
            //    bookEntities.Books.Add(new Books()
            //        {
            //            Name = "test",
            //            Author = "test",
            //            CreateDate = DateTime.UtcNow,
            //            Creater = "Gary",
            //            Description = "test",
            //            Id = Guid.NewGuid(),
            //            IsEnable = "1",
            //            PublishDate = DateTime.UtcNow,
            //            Publisher = "test Publisher",
            //            UpdateDate = DateTime.UtcNow,
            //            Updater = "Gary"
            //        });
            //}
            //bookEntities.SaveChanges();

            //var qlistBookBox = (from b in books
            //                    orderby b.UpdateDate descending
            //                    select new BookBox()
            //                        {
            //                            Id = b.Id,
            //                            Name = b.Name,
            //                            Author = b.Author,
            //                            Publisher = b.Publisher,
            //                            PublishDate = (DateTime)b.PublishDate
            //                        }).Take(10);

            //model.listBookBox = qlistBookBox.ToList();

            var model = new HomeIndex();
            model.ListMainBooks = new List<BookBox>();
            model.ListPopularBooks = new List<BookSmallBox>();
            model.ListArticleBooks = new List<BookSmallBox>();
            model.ListNonarticleBooks = new List<BookSmallBox>();

            Mapper.CreateMap<Books, BookBox>();

            var listBookBox = _bookEntities.Books.OrderByDescending(b => b.UpdateDate).Take(4).ToList();
            Mapper.Map(listBookBox, model.ListMainBooks);

            Mapper.Reset();
            Mapper.CreateMap<Books, BookSmallBox>();

            listBookBox = _bookEntities.Books.OrderByDescending(b => b.UpdateDate).Take(5).ToList();
            Mapper.Map(listBookBox, model.ListPopularBooks);

            listBookBox = _bookEntities.Books.Where(b => b.Class.Equals("文學")).OrderByDescending(b => b.UpdateDate).Take(5).ToList();
            Mapper.Map(listBookBox, model.ListArticleBooks);

            listBookBox = _bookEntities.Books.Where(b => !b.Class.Equals("文學")).OrderByDescending(b => b.UpdateDate).Take(5).ToList();
            Mapper.Map(listBookBox, model.ListNonarticleBooks);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "您的應用程式描述頁面。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "您的連絡頁面。";

            return View();
        }

        [HttpGet]
        public ActionResult ChangeLanguage(string language, string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(language))
                Response.AppendCookie(new HttpCookie("lang", language) { HttpOnly = true });

            return RedirectToLocal(returnUrl);
        }

        #region Helper
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
