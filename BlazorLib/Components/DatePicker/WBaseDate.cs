using BlazorLib.Components.Form;
using Microsoft.AspNetCore.Components;

namespace BlazorLib.Components.DatePicker
{
    public class WBaseDate : WFormBaseComponent<DateTime?>
    {

        /// <summary>
        /// The currently selected date (two-way bindable). If null, then nothing was selected.
        /// </summary>
        [Parameter]
        public DateTime? Date
        {
            get => _value;
            set => SetDate(value);
        }

        protected void SetDate(DateTime? date)
        {
            if (_value != null && date != null && date.Value.Kind == DateTimeKind.Unspecified)
            {
                date = DateTime.SpecifyKind(date.Value, _value.Value.Kind);
            }

            if (_value != date)
            {
                if (date is not null)
                {
                    _value = date;
                    DateChanged.InvokeAsync(_value);
                    return;
                }
                return;
            }
            return;
        }

        /// <summary>
        /// Fired when the Date changes.
        /// </summary>
        [Parameter]
        public EventCallback<DateTime?> DateChanged { get; set; }

        /// <summary>
        /// Defines on which day the week starts. Depends on the value of Culture.
        /// </summary>
        [Parameter]
        public DayOfWeek? FirstDayOfWeek { get; set; } = null;


        /// <summary>
        /// If you want do mark some days, pass they here.
        /// </summary>
        [Parameter]
        public IEnumerable<DateTime>? MarkedDays { get; set; }

        /// <summary>
        /// Fired when the month of the picker changes.
        /// <para>Specially useful for when you want mark some specific dates.</para>
        /// </summary>
        [Parameter]
        public EventCallback<DateTime?> MonthChanged { get; set; }

        [Parameter]
        public string DateFormat { get; set; } = "yyyy-MM-dd";
    }
}
