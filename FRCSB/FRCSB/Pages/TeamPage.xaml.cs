using System;
using System.Collections.Generic;

using Xamarin.Forms;

using FRCSB.FRC;

namespace FRCSB
{
	public partial class TeamPage : TabbedPage
	{
		public TeamModel Team;
		public TeamPage(TeamModel team)
		{
			Team = team;
			Title = Team.fullName;
			TeamService t = (TeamService)team;
			Children.Add(new TeamInfoPage(t));
			Children.Add(new MatchListPage(t));
			InitializeComponent();

		}
	}
}
