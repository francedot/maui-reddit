using Newtonsoft.Json;

namespace MauiReddit.Models
{
    public class Subreddit
    {
        [JsonProperty("display_name")]
        public string Title { get; set; }
        [JsonProperty("icon_img")]
        public string IconUrl { get; set; }
    }
}
