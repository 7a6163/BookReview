using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Models.TemplateModels
{
    public class BookBox
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
    }
}