using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

using System.Linq;
 
namespace FRCSB.FRC
{

	public class TeamModel :FRCObject
    {
        public string website { get; set; }
        public string name { get; set; }
        public string locality { get; set; }
        public int rookie_year { get; set; }
        public string region { get; set; }
        public int team_number { get; set; }
        public string location { get; set; }
        public string key { get; set; }
        public string country_name { get; set; }
        public string motto { get; set; }
        public string nickname { get; set; }
		public override string value { get { 
				return fullName; } }


        public List<string> sponsorList
        {
            get
            {
                if (name != null)
                {
                    return new List<string>(name.Split('/'));
                }
                else
                {
                    return new List<string>();
                }
            }
        }

        public string fullName { get { return team_number.ToString() + " " + nickname; } }

        public List<Match> matches { get; set; }

        public List<EventModel> events { get; set; }

        public List<Award> awards { get; set; }


       

        

    }
     
}
