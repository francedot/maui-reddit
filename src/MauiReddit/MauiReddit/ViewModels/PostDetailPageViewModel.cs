using MauiReddit.Extensions;
using MauiReddit.Models;
using MauiReddit.Services;

namespace MauiReddit.ViewModels
{
    public class PostDetailPageViewModel : PageViewModelBase
    {
        private readonly IRedditSource _redditSource;
        private bool _isLoading = true;
        private Post _post;
        private IList<Comment> _comments;

        public PostDetailPageViewModel()
        {
            _redditSource = ServiceProvider.GetService<IRedditSource>();
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public Post Post
        {
            get { return _post; }
            set { Set(ref _post, value); }
        }

        public IList<Comment> Comments
        {
            get { return _comments; }
            set { Set(ref _comments, value); }
        }

        public override async Task OnNavigatedToAsync()
        {
            Comments = await _redditSource.GetComments(Post.Subreddit, Post.Id);
            IsLoading = false;
        }

        public override Task OnNavigatedFromAsync()
        {
            throw new NotImplementedException();
        }
    }
}
