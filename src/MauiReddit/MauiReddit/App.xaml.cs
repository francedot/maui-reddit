using MauiReddit.Pages;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace MauiReddit
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new PostListPage() { Title = "MAUI Reddit" });
		}
	}
}
