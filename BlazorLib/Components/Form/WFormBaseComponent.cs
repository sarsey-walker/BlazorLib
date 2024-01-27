using BlazorLib.Components.Base;

namespace BlazorLib.Components.Form
{
    public abstract class WFormBaseComponent<TValue> : WBaseComponent
    {

        /// <summary>
        /// This is the component's value.
        /// </summary>
        protected static TValue? _value;
    }
}
