using MauiReddit.Extensions;
using MauiReddit.Models;
using MauiReddit.Services;

namespace MauiReddit.ViewModels
{
    public class PostListPageViewModel : PageViewModelBase
    {
        private readonly IRedditSource _redditSource;
        private bool _isLoading = true;
        private IList<Post> _posts;

        public PostListPageViewModel()
        {
            _redditSource = ServiceProvider.GetService<IRedditSource>();
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public IList<Post> Posts
        {
            get { return _posts; }
            set { Set(ref _posts, value); }
        }

        public override Task OnNavigatedFromAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task OnNavigatedToAsync()
        {
            Posts = await _redditSource.GetPosts("maui");
            IsLoading = false;
        }
    }
}
