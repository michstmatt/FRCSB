using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using FRCSB.FRC;

namespace FRCSB
{
	public partial class FRCSBPage : ContentPage
	{
		public ObservableValue<List<FRCObject>>items { get; set; }
		public ObservableValue<bool> ShowSearch { get; set; }
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


		public FRCSBPage()
		{
			ShowSearch = new ObservableValue<bool>(); ShowSearch.Value = false;
			items = new ObservableValue<List<FRCObject>>();
			IsBusy = new ObservableValue<bool>();

			BindingContext = this;
			InitializeComponent();


			LoadDataCommand.Execute(lsvItems);


			txtSearch.TextChanged += (sender, e) =>
			{
				string text = ((SearchBar)sender).Text ?? "";

				items.Value = FRCObjectListService.Singleton.queryFRCObjects(text);
			};

			lsvItems.ItemSelected += async (sender, e) =>
			  {
				if (lsvItems.SelectedItem == null)
					  return;


				  if (lsvItems.SelectedItem is TeamModel)
				  {
					ShowSearch.Value = false; txtSearch.Text = "";
					TeamModel team = (TeamModel)lsvItems.SelectedItem;
					((ListView)sender).SelectedItem = null;
					await Navigation.PushAsync(new TeamPage(team));
					  
				  }

			  };



			ToolbarItems.Add(new ToolbarItem("Search", null,  () =>
			{
				ShowSearch.Value = !ShowSearch.Value;
				if (!ShowSearch.Value)
				{
					txtSearch.Text = "";
				}
			}));






		}
		public async Task load()
		{
			items.Value= await FRCObjectListService.Singleton.getList("2016");
			this.InvalidateMeasure();
		}
			
		
	}
}
