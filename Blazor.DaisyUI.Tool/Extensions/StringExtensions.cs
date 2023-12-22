internal static class StringExtensions
{
    public static string? ToTitleCase(this string? source)
    {
        if (source == null) return null;
        return char.ToUpper(source[0]) + source[1..];
    }
}