using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

            HomeIndex model = new HomeIndex();
            var books = _bookEntities.Books;
            var qlistBookBox = (from b in books
                                orderby b.UpdateDate descending
                                select new BookBox()
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Author = b.Author,
                                        Publisher = b.Publisher,
                                        PublishDate = (DateTime)b.PublishDate
                                    }).Take(10);
            model.listBookBox = qlistBookBox.ToList();
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
    }
}
