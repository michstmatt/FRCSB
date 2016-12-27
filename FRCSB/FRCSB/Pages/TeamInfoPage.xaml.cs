using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

using FRCSB.FRC;
namespace FRCSB
{
	public partial class TeamInfoPage : ContentPage
	{
		private TeamService team { get; set; }
		public TeamInfoPage(TeamService t)
		{
			Title = "Info";
			team = t;
			if (team.team != null)
				load();
			BindingContext = team.team;
			InitializeComponent();
		}

		async Task load()
		{
			team.getTeamInfo(team.team.team_number);
		}
	}
}
