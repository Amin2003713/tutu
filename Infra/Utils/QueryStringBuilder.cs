using System.Web;

namespace Infra.Utils
{
    public static class QueryStringBuilder
    {
        /// <summary>
        /// Builds a request URL by appending query parameters or path segments based on the type of source list.
        /// </summary>
        /// <typeparam name="TType">The type of items in the sources list.</typeparam>
        /// <param name="url">The base URL to which parameters or segments will be added.</param>
        /// <param name="sources">A list of parameters (as dictionaries) or path segments (as strings).</param>
        /// <returns>The modified URL with appended query string or path segments.</returns>
        public static string? BuildRequestUrl<TType>(this string url, List<TType> sources)
        {
            if (url is null || sources.Count == 0)
                return url;

            // Check the type of TType and delegate to the appropriate private method
            if (typeof(TType) == typeof(Dictionary<string, string>))
            {
                return ToQueryString(url, sources.Cast<Dictionary<string, string>>().ToList());
            }

            if (typeof(TType) == typeof(string))
            {
                return ToQueryPath(url, sources.Cast<string>().ToList());
            }

            // If TType is neither Dictionary<string, string> nor string, return the original URL
            return url;
        }

        /// <summary>
        /// Converts a list of dictionaries into a query string and appends it to the URL.
        /// </summary>
        /// <param name="url">The base URL to which the query string will be appended.</param>
        /// <param name="filterParams">A list of dictionaries containing query parameters.</param>
        /// <returns>The URL with the query string appended.</returns>
        public static string ToQueryString<T>(this string url, T filterParams)
        {
            if (url is null || filterParams is null)
                return url!;

            // Get properties from the filterParams object using reflection
            var properties = typeof(T).GetProperties()
                .Where(prop => prop.GetValue(filterParams) != null) // Skip properties with null values
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(filterParams)?.ToString());

            // Create the query string
            var queryString = properties
                .Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}")
                .Aggregate(url.EndsWith("?") ? url : url + "?", (current, segment) => current + segment + "&");

            // Remove any trailing '&' from the query string
            return queryString.TrimEnd('&');
        }


        /// <summary>
        /// Converts a list of strings into a path and appends it to the URL.
        /// </summary>
        /// <param name="url">The base URL to which the path segments will be appended.</param>
        /// <param name="sources">A list of path segments to be appended.</param>
        /// <returns>The URL with the path segments appended.</returns>
        private static string? ToQueryPath(string url, List<string> sources)
        {
            if (url is null || sources.Count == 0)
                return url;

            // Create the path by joining the segments
            var pathSegments = sources
                .Select(segment => HttpUtility.UrlEncode(segment))
                .Aggregate(url.EndsWith("/") ? url : url + "/", (current, segment) => current + segment + "/");

            // Remove any trailing '/' from the path
            return pathSegments.TrimEnd('/');
        }
    }
}
