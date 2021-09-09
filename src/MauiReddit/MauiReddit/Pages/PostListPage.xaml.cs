using MauiReddit.Models;
using MauiReddit.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiReddit.Pages
{
	public partial class PostListPage : ContentPage
	{
		protected PostListPageViewModel ViewModel => this.BindingContext as PostListPageViewModel;

		public PostListPage()
		{
			InitializeComponent();

			this.BindingContext = new PostListPageViewModel();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
			await ViewModel.OnNavigatedToAsync();
        }

        private async void PostListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
			await Navigation.PushAsync(new PostDetailPage()
			{
				BindingContext = new PostDetailPageViewModel { Post = (Post)e.Item }
			});
        }
    }
}
