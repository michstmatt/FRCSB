using Xamarin.Forms;

namespace FRCSB
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();



			Color barColor = Color.White; object bc;
			Color barTextColor = Color.Black; object btc;
			Resources.TryGetValue("Primary", out bc);
			Resources.TryGetValue("TextIcon", out btc);
			barColor = (Color)bc; barTextColor = (Color)btc;




		MainPage = new NavigationPage(new FRCSBPage())
		{
				BarTextColor = barTextColor,
			BarBackgroundColor = barColor

		};
			NavigationPage.SetBackButtonTitle(MainPage, "Back");


		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
