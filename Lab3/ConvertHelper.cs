namespace Lab3
{
    public static class ConvertHelper
    {
        public static string GetBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
