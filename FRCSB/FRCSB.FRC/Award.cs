using System;
using System.Collections.Generic;
using System.Text;
namespace FRCSB.FRC
{
    public class RecipientList
    {
        public int team_number { get; set; }
        public string awardee { get; set; }
    }
    public class Award
    {
        public string event_key { get; set; }
        public int award_type { get; set; }
        public string name { get; set; }
        public List<RecipientList> recipient_list { get; set; }
        public int year { get; set; }
    }
}
