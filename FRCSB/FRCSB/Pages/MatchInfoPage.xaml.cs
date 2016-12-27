using System;
using System.Collections.Generic;

using Xamarin.Forms;

using FRCSB.FRC;

namespace FRCSB
{
	public partial class MatchInfoPage : ContentPage
	{
		public Match match { get; set; }

		public MatchInfoPage(Match m)
		{
			match = m;


			Title = match.matchNumber;

			BindingContext = match;
			InitializeComponent();
		}
	}
}
