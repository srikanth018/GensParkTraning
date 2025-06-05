namespace NotifyApp.Misc
{
    public static class FileHelper
{
    public static string GetContentTypes(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext switch
        {
            ".pdf" => "application/pdf",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".txt" => "text/plain",
            ".html" => "text/html",
            ".json" => "application/json",
            _ => "application/octet-stream",
        };
    }
}

}