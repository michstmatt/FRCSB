using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using FRCSB.FRC;
using Newtonsoft.Json;

namespace FRCSB.FRC
{
	public class EventService : IFRCService
    {
        private int lastSorted;
        public EventModel frcEvent { get; set; }
        private JsonSerializerSettings jss = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        public EventService(string key)
        {
            getEventInfo(key);
        }
        public EventService (EventModel em) { frcEvent = em; }

        public static explicit operator EventService (EventModel ev)
        {
            return new EventService(ev.key) { frcEvent = ev };
        }

        public async Task getEventInfo(string _key)
        {
            //frcEvent = new EventModel() { key = _key };
            frcEvent = JsonConvert.DeserializeObject<EventModel>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/event/" + _key),jss);
        }
        public async Task<List<TeamModel>> getEventTeams()
        {
            try
            {
                frcEvent.teams = JsonConvert.DeserializeObject<List<TeamModel>>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/event/"  + frcEvent.key + "/teams"));
                return frcEvent.teams;

            }
            catch (Exception e)
            {
                return new List<TeamModel>();
            }
        }

        public async Task<List<Match>> getMatches()
        {
            try
            {
				var json = await FRC.gethttp("http://www.thebluealliance.com/api/v2/event/" + frcEvent.key + "/matches");
                frcEvent.matches = JsonConvert.DeserializeObject<List<Match>>(json,jss);
                return frcEvent.matches;

            }
            catch (Exception e)
            {
                return new List<Match>();
            }
        }
        public async Task<List<EventGroup>> getMatchesGrouped()
        {
			if (frcEvent.matches == null || frcEvent.matches.Count == 0)
			{
				frcEvent.matches = await getMatches();
			}

            var matchGroup=(from m in frcEvent.matches
                    orderby m.time descending
                    group m by m.compLevel into grouped
                    select
                       new EventGroup(grouped)
                       {
                           Header = grouped.Key
                       }).ToList();
			return matchGroup;
        }



        public async Task<List<Award>> getEventAwards()
        {
            frcEvent.awards = JsonConvert.DeserializeObject<List<Award>>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/event/" + frcEvent.key + "/awards"), jss);
           
            return frcEvent.awards;
        }

        public async Task<List<List<string>>> getRankings()
        {
            frcEvent.rankData= JsonConvert.DeserializeObject<List<List<string>>>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/event/" + frcEvent.key + "/rankings"), jss);
            frcEvent.rankingKey = frcEvent.rankData.First(); frcEvent.rankData.RemoveAt(0);
            return frcEvent.rankData;
        }
        public List<List<string>> sortRankings(int index)
        {
            List<List<string>> sorted;
           
            try
            {
                
                sorted = (from data in frcEvent.rankData orderby double.Parse(data[index]) descending select data).ToList();
            }
            catch
            {
                sorted = (from data in frcEvent.rankData orderby data[index] descending select data).ToList();
            }
            lastSorted = index;
            return sorted.ToList();

        }
        #region PerTeam
        public async Task<List<Match>> getTeamMatches(TeamModel tm)
        {
            List<Match> tmpMatch;
            tmpMatch = JsonConvert.DeserializeObject<List<Match>>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/team/frc" + tm.team_number.ToString() + "/event/" + frcEvent.key + "/matches"));
            if (tmpMatch == null)
                tmpMatch = new List<Match>();
            return tmpMatch;
        }
        
        public async Task<List<Award>> getTeamAwards(TeamModel tm)
        {
            List<Award> tmpAward = new List<Award>();
            try
            {
                tmpAward = JsonConvert.DeserializeObject<List<Award>>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/team/frc" + tm.team_number.ToString() + "/event/" + frcEvent.key + "/awards"), jss);
               
            }
            catch (Exception ex) {
                DebuggerClass.reportError(ex.Message);
            }
            if (tmpAward == null)
                tmpAward = new List<Award>();
            frcEvent.awards = tmpAward;
            return tmpAward;
        }

        #endregion
    }
}