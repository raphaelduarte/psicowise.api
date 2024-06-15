namespace Psicowise.Helpers
{
    public static class HelperMethods
    {
        // Helper method to extract port from URL
        public static int GetPortFromUrl(string url)
        {
            if (url.StartsWith("http://*") || url.StartsWith("https://*"))
            {
                var uri = new Uri(url.Replace("*", "localhost"));
                return uri.Port;
            }

            return new Uri(url).Port;
        }
    }
}