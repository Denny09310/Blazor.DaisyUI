using Blazor.DaisyUI.Tool.Models;

namespace Blazor.DaisyUI.Tool.Contracts.Services
{
    internal interface IHeaderParser
    {
        int GetIndexOfHeaders(string content);
        IEnumerable<Header> ParseContent(string content);
        Header ParseHeader(string content);
    }
}