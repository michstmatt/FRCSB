/*using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.IO;
using Windows.Data.Xml.Dom;
using Windows.Web.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace FRCSB.FRC
{
    class RSSFeed
    {
        public static async Task<List<NewsItem>> grabRSS(Uri uri)
        {
            
          //  Uri uri = new Uri("http://www.chiefdelphi.com/forums/external.php?type=RSS2");

           
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            // fetch as string to avoid error
            HttpResponseMessage response = await client.GetAsync(uri);
            string data = await response.Content.ReadAsStringAsync();

            // convert to xml (will validate, too)
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(data);

            // manually fill feed from xml
            SyndicationFeed feed = new SyndicationFeed();
            feed.LoadFromXml(xmlDocument);

            // continue as usual...

            List<NewsItem> news = new List<NewsItem>();
            foreach (SyndicationItem item in feed.Items)
            {
                // do something
          
               news.Add(
                   new NewsItem()
                   {
                       Title=item.Title.Text,
                       PubDate=item.PublishedDate,
                       Url=item.Links[0].Uri,
                      Content=Regex.Replace(item.Summary.Text, "<.*?>", string.Empty)
                   }
                   
                   
                   );
            }
            return news;
        }
    }

    public class NewsItem
    {
        public string Title { get; set; }
        public DateTimeOffset PubDate { get; set; }
        public string Date
        {
            get
            {
                return PubDate.ToString("G");
            }
        }
        public Uri Url { get; set; }
        public string Content { get; set; }
    }
}
*/