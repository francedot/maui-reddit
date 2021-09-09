using MauiReddit.Extensions;
using MauiReddit.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiReddit.Services
{
    public class RedditApiSource : IRedditSource
    {
        private string BaseUriString { get; } = "https://www.reddit.com";

        public RedditApiSource()
        {
        }

        public async Task<IList<Subreddit>> GetSubreddits()
        {
            using (var httpClient =
                    new HttpClient(
                        new HttpClientHandler
                        {
                            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                        }))
            {
                var response = await httpClient.GetAsync($"{BaseUriString}/reddits.json");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<Subreddit>>(content);
                }
            }

            return default;
        }

        public async Task<IList<Post>> GetPosts(string subReddit)
        {
            using (var httpClient =
                    new HttpClient(
                        new HttpClientHandler
                        {
                            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                        }))
            {
                var response = await httpClient.GetAsync($"{BaseUriString}/r/{subReddit}/.json?limit=30&sort=new");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return DeserializePosts(content);
                }
            }

            return default;
        }

        private IList<Post> DeserializePosts(string content)
        {
            var result = new List<Post>();

            var responseJObject = JObject.Parse(content);
            var dataProperty = responseJObject["data"];
            var childrenProperty = dataProperty["children"];
            var posts = JArray.Parse(childrenProperty.ToString());

            foreach (var post in posts)
            {
                result.Add(DeserializePost(post.ToString()));
            }

            return result;
        }

        private Post DeserializePost(string postContent)
        {
            var postJObject = JObject.Parse(postContent);
            var dataProperty = postJObject["data"];
            var id = dataProperty["id"].ToString();
            var content = dataProperty["selftext"].ToString();
            var author = dataProperty["author"].ToString();
            var title = dataProperty["title"].ToString();
            var subreddit = dataProperty["subreddit"].ToString();
            var createdUtc = dataProperty["created_utc"].ToString();
            var createdDateLong = long.Parse(createdUtc);
            var createdDate = createdDateLong.ToDateTimeFromEpoch();
            var likesRaw = dataProperty["ups"].ToString();
            var likes = string.IsNullOrEmpty(likesRaw) ? 0 : int.Parse(likesRaw);
            var numCommentsRaw = dataProperty["num_comments"].ToString();
            var numComments = string.IsNullOrEmpty(numCommentsRaw) ? 0 : int.Parse(numCommentsRaw);
            var thumbnailUri = dataProperty["thumbnail"].ToString();
            if (string.IsNullOrEmpty(thumbnailUri) || thumbnailUri.Trim() == "self")
            {
                thumbnailUri = "Assets/Placeholder.jpg";
            }

            return new Post()
            {
                Id = id,
                Title = title,
                Subreddit = subreddit,
                Content = content,
                Author = author,
                DateCreated = createdDate,
                Likes = likes,
                CommentsCount = numComments,
                ThumbnailUri = thumbnailUri
            };
        }

        public async Task<IList<Comment>> GetComments(string subreddit, string postId)
        {
            using (var httpClient =
                    new HttpClient(
                        new HttpClientHandler
                        {
                            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                        }))
            {
                var response = await httpClient.GetAsync($"{BaseUriString}/r/{subreddit}/comments/{postId}.json");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return DeserializeComments(content);
                }
            }

            return default;
        }

        private IList<Comment> DeserializeComments(string content)
        {
            var result = new List<Comment>();

            var responseJArray = JArray.Parse(content);
            var responseJObject = JObject.Parse(responseJArray[1].ToString());
            var dataProperty = responseJObject["data"];
            var childrenProperty = dataProperty["children"];
            var comments = JArray.Parse(childrenProperty.ToString());

            foreach (var comment in comments)
            {
                result.Add(DeserializeComment(comment.ToString()));
            }

            return result;
        }

        private Comment DeserializeComment(string commentContent)
        {
            var postJObject = JObject.Parse(commentContent);
            var dataProperty = postJObject["data"];
            var author = dataProperty["author"]?.ToString();
            var content = dataProperty["body"]?.ToString();
            var createdUtc = dataProperty["created_utc"]?.ToString();
            var createdDateLong = createdUtc == null ? 0 : long.Parse(createdUtc);
            var createdDate = createdDateLong.ToDateTimeFromEpoch();

            return new Comment()
            {
                Content = content,
                Author = author,
                DateCreated = createdDate
            };
        }
    }
}
