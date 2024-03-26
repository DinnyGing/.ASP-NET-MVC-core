namespace Lab3
{
    public static class DateHelper
    {
        public static string FormatDate(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("yyyy-MM-dd") : string.Empty;
        }
    }
}
