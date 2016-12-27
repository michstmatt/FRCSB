using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using FRCSB.FRC;
using Newtonsoft.Json;
namespace FRCSB.FRC
{
    public class TeamService :IFRCService
    {
        public TeamModel team { get; set; }
        private JsonSerializerSettings jss = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };


        public TeamService(int num)
        {
            getTeamInfo(num);
        }
        public TeamService(TeamModel t) { 
			team = t; 
			getTeamInfo(team.team_number); }
		public static explicit operator TeamService(TeamModel t)
		{
			return new TeamService(t);

		}

        public async void getTeamInfo(int num)
        {
            team = JsonConvert.DeserializeObject<TeamModel>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/team/frc" + num.ToString()));
        }

        public async Task<List<EventModel>> getEvents()
        {
            
           var result =JsonConvert.DeserializeObject<List<EventModel>>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/team/frc" + team.team_number.ToString() + "/" + GlobalSettings.year + "/events"),jss);
            return team.events=result;
        }

        public async Task<List<Match>> getMatches()
        {
            if (team.events == null)
                await getEvents();
			
            team.matches = new List<Match>();

            foreach (EventService ev in team.events)
            {
                team.matches.AddRange(await ev.getTeamMatches(team));
            }
           
            return team.matches;

        }

        public async Task<List<EventGroup>> getMatchesGrouped()
        {
           team.matches= await getMatches();
           return( from m in team.matches
                             orderby m.time descending
                             group m by m.event_key into grouped
                             select
                                new EventGroup(grouped)
                                {
                                Header = grouped.Key
                                }).ToList();
        }

        public async Task<List<Award>> getAwards()
        {
            EventService ev;
            if (team.events == null)
                await getEvents();
            team.awards = new List<Award>();
            foreach (EventModel em in team.events)
            {
                ev = new EventService(em);
               
                team.awards.AddRange(await ev.getTeamAwards(team));
                   

            }
            return team.awards;
        }
    }
}