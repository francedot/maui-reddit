using System;
using MauiReddit.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiReddit.Pages
{
	public partial class PostDetailPage : ContentPage
	{
		protected PageViewModelBase ViewModel => this.BindingContext as PageViewModelBase;

		public PostDetailPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await ViewModel.OnNavigatedToAsync();
		}
	}
}
