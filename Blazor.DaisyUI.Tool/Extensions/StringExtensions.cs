internal static class StringExtensions
{
    public static string ToTitleCase(this string? source)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(source);
        return char.ToUpper(source[0]) + source[1..];
    }
}