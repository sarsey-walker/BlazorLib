using Microsoft.AspNetCore.Components;

namespace BlazorLib.Components.Base
{
    public partial class BaseComponent
    {
        [Parameter]
        public string? Style { get; set; }
        [Parameter]
        public string? Class { get; set; }
    }
}
