namespace AppointmentApp.Misc
{
    public static class FileContentTypes
    {
        public static string GetContentTypes(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return ext switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".txt" => "text/plain",
                ".html" => "text/html",
                _ => "application/octet-stream",
            };
        }
    }
}