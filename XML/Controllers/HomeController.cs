using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using System.Xml.Linq;
using System.Linq;
using XML.Models;
using System.Text.RegularExpressions;


namespace XML.Controllers
{
    public class HomeController : Controller
    {
        private XDocument doc;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListRSS(string pageName)
        {
            doc = XDocument.Load("C:\\Users\\hautv\\Documents\\Visual Studio 2013\\Projects\\GuiThay\\XML\\XML\\Models\\RSSList.xml");
            var RSSList = (from x in doc.Descendants("Page")
                           where (string)x.Parent.Attribute("name").Value == pageName
                           select new RSSCategory
                           {
                               Category = (string)x.Element("Category"),
                               Link = (string)x.Element("Link")
                           }
            );

            ViewBag.RSSList = RSSList;
            return View();
        }

        [HttpGet]
        public ActionResult ListFeed(string RSSURL)
        {
            if (RSSURL != null)
            {
                doc = XDocument.Load(RSSURL);
                var RSSFeedData = (from x in doc.Descendants("item")
                                   select new RSSFeed
                                   {
                                       Title = (string)x.Element("title"),
                                       Link = (string)x.Element("link"),
                                       Description = Regex.Split((string)x.Element("description"),"</a>")[0] + "</a>",
                                       PubDate = (string)x.Element("pubDate")
                                   });

                ViewBag.RSSFeed = RSSFeedData;
                ViewBag.URL = RSSURL;
            }

            return View();
        }
    }
}