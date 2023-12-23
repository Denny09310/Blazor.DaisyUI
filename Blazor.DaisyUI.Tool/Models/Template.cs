namespace Blazor.DaisyUI.Tool.Models;

internal class Template
{
    public string Content { get; set; } = string.Empty;
    public IEnumerable<Header> Headers { get; set; } = [];
}