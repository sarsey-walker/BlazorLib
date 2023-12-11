using Microsoft.AspNetCore.Components;

namespace BlazorLib.Components.Base
{
    public class BaseComponent<T> : ComponentBase
    {
        [Parameter]
        public string? Style { get; set; }
        [Parameter]
        public string? Class { get; set; }
    }
}
