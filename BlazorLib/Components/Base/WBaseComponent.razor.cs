using Microsoft.AspNetCore.Components;

namespace BlazorLib.Components.Base
{
    public abstract class WBaseComponent : ComponentBase
    {
        /// <summary>
        /// User styles, applied on top of the component's own classes and styles.
        /// </summary>
        [Parameter]
        public string? Style { get; set; }

        /// <summary>
        /// User class names, separated by space.
        /// </summary>
        [Parameter]
        public string? Class { get; set; }
    }
}
