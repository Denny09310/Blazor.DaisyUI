using Blazor.DaisyUI.Tool.Contracts.Services;
using Blazor.DaisyUI.Tool.Models;

namespace Blazor.DaisyUI.Tool.Services;

internal class HeaderParser : IHeaderParser
{
    private const string HeaderEndSequence = "#>";
    private const string HeaderStartSequence = "<#";

    public int GetIndexOfHeaders(string content) => content.LastIndexOf(HeaderEndSequence) + HeaderEndSequence.Length;

    public IEnumerable<Header> ParseContent(string content)
    {
        var indexOfHeader = GetIndexOfHeaders(content);

        if (indexOfHeader != -1)
        {
            var header = content[..indexOfHeader];
            var headerLines = header.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in headerLines)
            {
                yield return ParseHeader(line);
            }
        }
    }

    public Header ParseHeader(string content)
    {
        var formattedContent = content
            .Replace(HeaderStartSequence, string.Empty)
            .Replace(HeaderEndSequence, string.Empty)
            .Trim();

        return formattedContent.Split('=', StringSplitOptions.TrimEntries) switch
        {
            [var key, var value] => new Header(key, value.Replace("\"", string.Empty)),
            _ => throw new NotImplementedException()
        };
    }
}