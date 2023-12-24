using Blazor.DaisyUI.Tool.Contracts.Services;
using Blazor.DaisyUI.Tool.Models;
using System.Collections.Concurrent;

namespace Blazor.DaisyUI.Tool.Services;

internal class DependencyStore(IHeaderParser parser) : IDependencyStore
{
    public ConcurrentDictionary<string, Template> Dependencies { get; } = new();

    public void AddDependency(string name, string content, IEnumerable<Header> headers)
    {
        if (Dependencies.ContainsKey(name)) return;

        var headersIndex = parser.GetIndexOfHeaders(content);

        content = content[headersIndex..];
        var template = new Template { Content = content, Headers = headers };

        if (!Dependencies.TryAdd(name, template))
            throw new Exception("Can't add template to the dependecies");
    }
}