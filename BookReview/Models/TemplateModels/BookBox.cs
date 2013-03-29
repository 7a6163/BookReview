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
        private string _language;
        public string Language
        {
            get { return _language; }
            set { _language = value.Equals("0") ? "中文" : "英文"; }
        }
        public string Binding { get; set; }
        public int Price { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
    }
}