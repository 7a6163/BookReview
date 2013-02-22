using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using Domain; 

namespace BookCrawler
{
    public partial class Form1 : Form
    {
        private readonly BookEntities _bookEntities = new BookEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlWeb webClient = new HtmlWeb()
                {
                    AutoDetectEncoding = false,
                    OverrideEncoding = Encoding.GetEncoding(950)
                };

            int start = 0010569456;
            int end = 0010569460;

            string dateFormat = "yyyy年MM月dd日";
            for (int i = start; i < end; i++)
            {
                string url = string.Format("http://www.books.com.tw/exep/prod/booksfile.php?item={0}",
                                           i.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'));
                HtmlDocument doc =  webClient.Load(url);

                var count = doc.DocumentNode.SelectNodes(@"//*[@id=""pr_data""]/ul[1]/li").Count;

                string dateString =
                    doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[4]/dfn") != null
                        ? doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[4]/dfn").InnerText
                        : doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[5]/dfn").InnerText;

                Books book = new Books();

                book.Id = Guid.NewGuid();
                book.Name = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/div[1]/h1/span").InnerText;
                book.Author = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[1]/a").InnerText;
                book.Publisher = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[2]/a") != null
                                     ? doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[2]/a")
                                          .InnerText
                                     : doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[4]/a")
                                          .InnerText;

                book.PublishDate = DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
                book.Description = "Nothing";
                book.CreateDate = DateTime.Now;
                book.Creater = "Zac";
                book.UpdateDate = DateTime.Now;
                book.Updater = "Zac";
                book.IsEnable = "1";

                _bookEntities.Books.Add(book);
                _bookEntities.SaveChanges();

            }
        }
    }
}
