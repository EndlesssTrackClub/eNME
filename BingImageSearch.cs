using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace eNME
{
    static class BingImageSearch
    {
        public static string SubscriptionKey;
        public static List<string> RunQuery(string queryString)
        {
            var uriQuery = "https://api.bing.microsoft.com/" + "/v7.0/images/search" + "?q=" + Uri.EscapeDataString(queryString);

            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = SubscriptionKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var parsedJson = JsonConvert.DeserializeObject<Schema.Root>(json);

            return parsedJson.value.Select( x => x.contentUrl ).ToList();

        }
    }

    namespace Schema
    {
        public class InsightsMetadata
        {
            public int pagesIncludingCount { get; set; }
            public int availableSizesCount { get; set; }
            public int? recipeSourcesCount { get; set; }
        }

        public class Instrumentation
        {
            public string _type { get; set; }
        }

        public class PivotSuggestion
        {
            public string pivot { get; set; }
            public List<object> suggestions { get; set; }
        }

        public class QueryContext
        {
            public string originalQuery { get; set; }
            public string alterationDisplayQuery { get; set; }
            public string alterationOverrideQuery { get; set; }
            public string alterationMethod { get; set; }
            public string alterationType { get; set; }
        }

        public class RelatedSearch
        {
            public string text { get; set; }
            public string displayText { get; set; }
            public string webSearchUrl { get; set; }
            public string searchLink { get; set; }
            public Thumbnail thumbnail { get; set; }
        }

        public class Root
        {
            public string _type { get; set; }
            public Instrumentation instrumentation { get; set; }
            public string readLink { get; set; }
            public string webSearchUrl { get; set; }
            public QueryContext queryContext { get; set; }
            public int totalEstimatedMatches { get; set; }
            public int nextOffset { get; set; }
            public int currentOffset { get; set; }
            public List<Value> value { get; set; }
            public List<PivotSuggestion> pivotSuggestions { get; set; }
            public List<RelatedSearch> relatedSearches { get; set; }
        }

        public class Thumbnail
        {
            public int width { get; set; }
            public int height { get; set; }
            public string thumbnailUrl { get; set; }
        }

        public class Value
        {
            public string webSearchUrl { get; set; }
            public string name { get; set; }
            public string thumbnailUrl { get; set; }
            public DateTime datePublished { get; set; }
            public bool isFamilyFriendly { get; set; }
            public string contentUrl { get; set; }
            public string hostPageUrl { get; set; }
            public string contentSize { get; set; }
            public string encodingFormat { get; set; }
            public string hostPageDisplayUrl { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public DateTime hostPageDiscoveredDate { get; set; }
            public Thumbnail thumbnail { get; set; }
            public string imageInsightsToken { get; set; }
            public InsightsMetadata insightsMetadata { get; set; }
            public string imageId { get; set; }
            public string accentColor { get; set; }
            public string hostPageFavIconUrl { get; set; }
            public string hostPageDomainFriendlyName { get; set; }
            public bool? isFresh { get; set; }
        }
    }

}
