using Blazor.DaisyUI.Tool.Models;
using System.Collections.Concurrent;

namespace Blazor.DaisyUI.Tool.Contracts.Services
{
    internal interface IDependencyStore
    {
        ConcurrentDictionary<string, Template> Dependencies { get; }

        void AddDependency(string name, string content, IEnumerable<Header> headers);
    }
}