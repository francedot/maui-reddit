using MauiReddit.Models;

namespace MauiReddit.Services
{
    public interface IRedditSource
    {
        Task<IList<Subreddit>> GetSubreddits();
        Task<IList<Post>> GetPosts(string subReddit);
        Task<IList<Comment>> GetComments(string subreddit, string postId);
    }
}
