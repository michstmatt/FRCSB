using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
namespace FRCSB.FRC
{

    public class Webcast
    {
        public string type { get; set; }
        public string channel { get; set; }

        public string uri
        {
            get
            {
                if (type == "twitch")
                    return string.Format("http://www.twitch.tv/{0}", channel);
                else if (type == "html5")
                    return channel;
                else if (type == "iframe")
                {
                    int index = channel.IndexOf('\"');
                    string val= channel.Substring(index+1, channel.IndexOf('"', index + 1) - index);
                    return val;
                }
                else if (type == "youtube")
                    return string.Format("http://www.youtube.com/watch?v={0}", channel);
                else
                    return channel;
            }
        }
    }

    public class Alliance
    {
        public List<object> declines { get; set; }
        public List<string> picks { get; set; }
    }

    public class EventModel :FRCObject
	{
        public string key { get; set; }
        public string website { get; set; }
        public bool official { get; set; }
        public DateTime end_date { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public object facebook_eid { get; set; }
        public object event_district_string { get; set; }
        public string venue_address { get; set; }
        public int event_district { get; set; }
        public string location { get; set; }
        public string event_code { get; set; }
        public int year { get; set; }
        public List<Webcast> webcast { get; set; }
        public string timezone { get; set; }
        public List<Alliance> alliances { get; set; }
        public string event_type_string { get; set; }
        public DateTime start_date { get; set; }
        public int event_type { get; set; }

        public List<TeamModel> teams { get; set; }

        public List<Match> matches { get; set; }
        public List<Award> awards { get; set; }
        
        public List<string> rankingKey { get; set; }
        public List<List<string>> rankData { get; set; }


		public override string value { get { return name;}}








    }



}

