using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Collections.ObjectModel;
namespace FRCSB.FRC
{
    class FRC
    {
        public static async Task<string> gethttp(string uri, string modified="")
        {
            HttpClient httpClient;

            httpClient = new HttpClient();
            // Limit the max buffer size for the response so we don't get overwhelmed
            httpClient.MaxResponseContentBufferSize = 256000;
           // httpClient.DefaultRequestHeaders.c
          // httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
           httpClient.DefaultRequestHeaders.Add("X-TBA-App-Id", "windows_frc:scrape_team_info:v1");
            if (modified == "")
                modified = DateTime.MinValue.ToUniversalTime().ToString()+" +00:00";
          //  httpClient.DefaultRequestHeaders.Add("If-Modified-Since", DateTime.Now.ToString());
            HttpResponseMessage response =await httpClient.GetAsync(uri);
            Stream result = await response.Content.ReadAsStreamAsync();
            return await new StreamReader(result).ReadToEndAsync() ;
        }
    }
    public class EventGroup : ObservableCollection<object>
    {
        public EventGroup(IEnumerable<object> items) : base(items)
        {
        }

        public string Header { get; set; }
    }
}
