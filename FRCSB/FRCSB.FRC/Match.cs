using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
namespace FRCSB.FRC
{

    public class Video
    {
        public string type { get; set; }
        public string key { get; set; }
    }

    

    public class MatchAlliance
    {
        public int score { get; set; }
        public List<string> fixedTeams { get
            {
                List<string> temp= new List<string>();
                foreach (string s in teams)
                    temp.Add(s.Replace("frc", ""));
                return temp;


            } }
        public List<string> teams { get; set; }
    }

    public class Alliances
    {
        public MatchAlliance blue { get; set; }
        public MatchAlliance red { get; set; }
    }

    public class Match
    {
        public string comp_level { get; set; }

        public string compLevel
        {
            get
            {
                if (comp_level == "f")
                    return "Finals";
                else if (comp_level == "sf")
                    return "Semifinals";
                else if (comp_level == "qf")
                    return "Quarterfinals";

                else if (comp_level == "qm")
                    return "Qualifications";
                else
                    return "Other";
            }
        }
        public int match_number { get; set; }
        public string matchNumber { get { return compLevel + " " + match_number.ToString(); } }
        public List<Video> videos { get; set; }
        public int set_number { get; set; }
        public string key { get; set; }
        public int time { get; set; }
		public string timeString { get { return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(time).ToLocalTime().ToString("MM/dd hh:mm"); } }

		public object score_breakdown { get; set; }
        public Alliances alliances { get; set; }
        public string event_key { get; set; }
    }





}
