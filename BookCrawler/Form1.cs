using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
            var webClient = new HtmlWeb
                {
                    AutoDetectEncoding = false,
                    OverrideEncoding = Encoding.GetEncoding(950)
                };

            const int start = 0010560000;
            const int end = 0010570000;

            const string dateFormat = "yyyy年MM月dd日";
            for (int i = start; i < end; i++)
            {
                string url = string.Format("http://www.books.com.tw/exep/prod/booksfile.php?item={0}",
                                           i.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'));

                HtmlDocument doc = webClient.Load(url);

                if (doc.DocumentNode.SelectNodes(@"//*[@id=""pr_data""]/ul[1]/li") == null) continue;

                string subClass =
                    doc.DocumentNode.SelectSingleNode(@"//*[@class=""here""]/a").InnerText;

                string mainClass =
                    doc.DocumentNode.SelectSingleNode(@"//*[@class=""layer1""]").PreviousSibling.InnerHtml;

                int count = doc.DocumentNode.SelectNodes(@"//*[@id=""pr_data""]/ul[1]/li").Count;

                var book = new Books
                    {
                        Id = Guid.NewGuid(),
                        Name = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/div[1]/h1/span").InnerText,
                        Author = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[1]/a").InnerText,
                        Description = doc.DocumentNode.SelectSingleNode(@"//*[@id=""tag1content""]").InnerHtml
                    };
                string sUrl = doc.DocumentNode.SelectSingleNode(@"//*[@id=""main_img""]")
                                   .GetAttributeValue("src", string.Empty);

                var regex = new Regex("image=(?<url>.+jpg)", RegexOptions.None);
                var matches = regex.Match(sUrl);
                string imgUrl = matches.Groups["url"].Value;
                using (var client = new WebClient())
                {
                    if (string.IsNullOrEmpty(imgUrl)) continue;
                    client.DownloadFile(imgUrl, string.Format("{0}.jpg", book.Id));
                }


                string price =
                    doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/div[2]/div/ul/li[1]/dfn[1]/u").InnerText;
                book.Price = int.Parse(price);

                string dateString = string.Empty;
                string language = string.Empty;
                bool hasIsbn = true;
                switch (count)
                {
                    case 8:
                        dateString = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[6]/dfn").InnerText;
                        book.Publisher = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[5]/a").InnerText;
                        if (doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[7]/dfn[2]") == null)
                        {
                            hasIsbn = false;
                            break;
                        }
                        book.ISBN =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[7]/dfn[2]").InnerText;
                        language =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[7]/dfn[1]").InnerText;
                        book.Language = Equals(language, "繁體中文") ? "0" : "1";
                        book.Binding =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[8]/dfn").InnerText;
                        break;
                    case 7:
                        dateString = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[5]/dfn").InnerText;
                        book.Publisher = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[4]/a").InnerText;
                        if (doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[6]/dfn[2]") == null)
                        {
                            hasIsbn = false;
                            break;
                        }
                        book.ISBN =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[6]/dfn[2]").InnerText;
                        language =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[6]/dfn[1]").InnerText;
                        book.Language = Equals(language, "繁體中文") ? "0" : "1";
                        book.Binding =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[7]/dfn").InnerText;

                        break;
                    case 6:
                        dateString = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[4]/dfn").InnerText;
                        book.Publisher = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[3]/a").InnerText;
                        if (doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[5]/dfn[2]") == null)
                        {
                            hasIsbn = false;
                            break;
                        }
                        book.ISBN =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[5]/dfn[2]").InnerText;
                        language =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[5]/dfn[1]").InnerText;
                        book.Binding =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[6]/dfn").InnerText;
                        break;
                    case 5:
                        dateString = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[3]/dfn").InnerText;
                        book.Publisher = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[2]/a").InnerText;

                        if (doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[4]/dfn[2]") == null)
                        {
                            hasIsbn = false;
                            break;
                        }

                        book.ISBN = doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[4]/dfn[2]").InnerText;

                        language =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[4]/dfn[1]").InnerText;
                        book.Binding =
                            doc.DocumentNode.SelectSingleNode(@"//*[@id=""pr_data""]/ul[1]/li[5]/dfn").InnerText;
                        break;
                }

                if (!hasIsbn)
                {
                    continue;
                }

                book.Language = Equals(language, "繁體中文") ? "0" : "1";
                book.PublishDate = DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
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
