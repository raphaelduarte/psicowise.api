namespace Psicowise.Helpers
{
    public static class HelperMethods
    {
        // Helper method to extract port from URL
        public static int GetPortFromUrl(string url)
        {
            return new Uri(url).Port;
        }
    }
}