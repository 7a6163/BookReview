using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReview.Models;
using BookReview.Models.ViewModels;
using Domain;

namespace BookReview.Controllers
{
    public class BookController : Controller
    {
        private readonly BookEntities _bookEntities = new BookEntities();

        //
        // GET: /Book/

        public ActionResult Index(Guid? id)
        {
            return View();
        }

        public ActionResult BookDetail(Guid? id)
        {
            var book = _bookEntities.Books.Find(id);
            BookBookDetail model = new BookBookDetail()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Name = book.Name,
                    PublishDate = (DateTime)book.PublishDate,
                    Publisher = book.Publisher
                };
            return View(model);
        }
    }
}
