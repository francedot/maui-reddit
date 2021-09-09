using MauiReddit.Extensions;
using Newtonsoft.Json;

namespace MauiReddit.Models
{
    public class Post
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }
        [JsonProperty("selftext")]
        public string Content { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("created_utc")]
        public long DateCreatedEpoch { get; set; }
        public DateTime DateCreated { get; set; }
        [JsonProperty("ups")]
        public int Likes { get; set; }
        [JsonProperty("num_comments")]
        public int CommentsCount { get; set; }
        [JsonProperty("thumbnail")]
        public string ThumbnailUri { get; set; }
    }
}
