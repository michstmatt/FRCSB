using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace FRCSB.FRC
{
    public class FRCObjectListService
    {
        private JsonSerializerSettings jss = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        private static FRCObjectListService singleton;
        public static FRCObjectListService Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new FRCObjectListService();

                return singleton;
            }
        }

        public List<TeamModel> teams;
        public List<EventModel> events;
        public List<EventGroup> weeklyEvents;
        public bool dataLoaded;
       
        public FRCObjectListService() { dataLoaded = false; }
        public async Task<List<FRCObject>> getList(string year)
        {
			List<FRCObject> returnedData = new List<FRCObject>();
            try
            {
             //   teams = await FRCFiles.Deserialize<List<TeamModel>>(year + "teams");
              //  events = await FRCFiles.Deserialize<List<EventModel>>(year + "events");
            }
            catch
            {
                
            }
            
            if (teams == null || teams.Count == 0 || events == null || events.Count==0)
            {
				teams = (await getTeamListing(year)).ToList();

                events = await getEventListing(year);

              //  FRCFiles.Serialize(teams, year + "teams");
             //   FRCFiles.Serialize(events, year + "events");
            }
            dataLoaded = true;
			returnedData.AddRange(teams ); returnedData.AddRange(events);
			return returnedData;
        }
        public async Task<IEnumerable<TeamModel>> getTeamListing(string year)
        {
            

            try
            {
                
				return (await Task.WhenAll(Enumerable.Range(0, 11).Select(async i => JsonConvert.DeserializeObject<IEnumerable<TeamModel>>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/teams/" + i.ToString()), jss)))).SelectMany(x => x).Where(x=>x.name!=null);
										
                
            }
            catch (Exception e)
            {
                DebuggerClass.reportError("An error occured at getTeamListing with error: " + e.Message);
            }
			return null;
            
            
        }
        public async Task<List<EventModel>> getEventListing(string year)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<EventModel>>(await FRC.gethttp("http://www.thebluealliance.com/api/v2/events/" + year),jss);
            }
            catch (Exception e)
            {
                DebuggerClass.reportError("An error occured at getEventListing with error: " + e.Message);
            }
            return new List<EventModel>();
        }



        public  List<FRCObject> queryFRCObjects(string query)
        {
           
            
            query = query.ToLower();
            
                List<FRCObject> result = new List<FRCObject>();
            try
            {
                result.AddRange(from team in singleton.teams
                                where (team.nickname != null && team.nickname.ToLower().Contains(query)) || team.team_number.ToString().Contains(query)
                                select (FRCObject)team);

                result.AddRange(from ev in singleton.events
                                where ev.name.ToLower().Contains(query) || (ev.short_name != null && ev.short_name.ToLower().Contains(query))
                                || ev.event_code.ToLower().Contains(query)
                                select (FRCObject)ev);
            }
            catch { }
            return result;                                  
        }


        public List<EventGroup>getWeeklyEvents()
        {

			events = (from e in events orderby e.start_date ascending select e).ToList();
			weeklyEvents = (from e in events
							group e by (e.start_date.DayOfYear - events.First().start_date.DayOfYear) / 7 into grouped
							select new EventGroup(grouped)
							{
								Header = $"Week {grouped.Key}" 
							}).ToList();
			return weeklyEvents;
        }

        public EventModel getNextEvent()
        {
            EventModel e = (from ev in events where ev.end_date.DayOfYear >= DateTime.Now.DayOfYear select ev).First();
            return e;
        }



    }
}