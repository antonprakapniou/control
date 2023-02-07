using Microsoft.AspNetCore.Mvc;

namespace Control.BLL.ViewModels;

[Serializable]
[BindProperties]
public sealed class FilterResponse
{
    public FilterVM? Filter { get; set; }
    public Guid[]? Ids { get; set; }
    public FilterResponse() { }
}
