namespace MyProject.Helpers
{
    public static class TagHelper
    {
        public static string GetIdFromTag(string tag)
        {
            int startPoint = tag.IndexOf("\"") + 1;
            int endPOint = tag.LastIndexOf("\"");

            return tag
                .Substring(startPoint, endPOint - startPoint)
                .Replace("-", "")
                .Trim(new[] { '{', '}' });
        }
    }
}