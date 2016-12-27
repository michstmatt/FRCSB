using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using FRCSB.FRC;

namespace FRCSB
{
	public partial class MatchListPage : ContentPage
	{
		private IFRCService service;
		public ObservableValue<List<EventGroup>> Matches { get; set; }
		public ObservableValue<bool> IsBusy { get; set; }


		private async Task ExecuteLoadDataCommand()
		{
			if (IsBusy.Value)
				return;

			IsBusy.Value = true;
			LoadDataCommand.ChangeCanExecute();

			await load();

			IsBusy.Value = false;
			LoadDataCommand.ChangeCanExecute();
		}


		private Command loadDataCommand;

		public Command LoadDataCommand
		{
			get
			{
				return loadDataCommand ?? (loadDataCommand = new Command(async () =>
				  {
					  await ExecuteLoadDataCommand();
				  }, () =>
			   {
				   return !IsBusy.Value;
			   }));
			}
		}

		public MatchListPage(IFRCService serv)
		{
			
			Title = "Matches";
			service = serv;
			IsBusy = new ObservableValue<bool>();
			Matches = new ObservableValue<List<EventGroup>>();
			Matches.Value = new List<EventGroup>();

			BindingContext = this;
			InitializeComponent();

			LoadDataCommand.Execute(this);
			lsvMatches.ItemSelected += async (sender, e) =>
			{
				if (lsvMatches.SelectedItem == null)
					return;

				Match m = (Match)lsvMatches.SelectedItem;
				lsvMatches.SelectedItem = null;

				await Navigation.PushAsync(new MatchInfoPage(m));

			};

		}
		public async Task load()
		{
			Matches.Value = await service.getMatchesGrouped();
		}
	}
}
