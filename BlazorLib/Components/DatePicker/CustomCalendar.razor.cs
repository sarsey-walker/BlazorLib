using Microsoft.AspNetCore.Components;
using System.Globalization;
using BlazorLib.Components.Form;

namespace BlazorLib.Components.DatePicker
{
    public partial class CustomCalendar : WFormBaseComponent<DateTime?>
    {

        /// <summary>
        /// The currently selected date (two-way bindable). If null, then nothing was selected.
        /// </summary>
        [Parameter]
        public DateTime? Date
        {
            get => _value;
            set => SetDate(value, true);
        }

        /// <summary>
        /// Defines on which day the week starts. Depends on the value of Culture.
        /// </summary>
        [Parameter]
        public DayOfWeek? FirstDayOfWeek { get; set; } = null;

        /// <summary>
        /// Fired when the DateFormat changes.
        /// </summary>
        [Parameter] 
        public EventCallback<DateTime?> SelectedDateChanged { get; set; }

        /// <summary>
        /// If you want do mark some days, pass they here.
        /// </summary>
        [Parameter]
        public IEnumerable<DateTime>? MarkedDays { get; set; }

        private DateTime currentDate = DateTime.Today;
        private DateTime currentMonth => new DateTime(currentDate.Year, currentDate.Month, 1);
        private int currentYear => currentMonth.Year;
        private int daysInMonth => DateTime.DaysInMonth(currentYear, currentMonth.Month);

        private List<DateTime> days = null!;

        protected override void OnInitialized()
        {
            if(Date ==  null)
            {
                Date = DateTime.Today;
            }
            UpdateDays();
        }

        private void UpdateDays()
        {
            days = new List<DateTime>();
            int firstDayOfWeek = (int)currentMonth.DayOfWeek;
            for (int i = 0; i < firstDayOfWeek; i++)
            {
                days.Add(DateTime.MinValue);
            }
            for (int i = 0; i < daysInMonth; i++)
            {
                days.Add(new DateTime(currentYear, currentMonth.Month, i + 1));
            }
        }


        private async void SelectDate(int index)
        {
            Date = days[index];
            await SelectedDateChanged.InvokeAsync(Date);
        }

        private void PreviousMonth()
        {
            currentDate = currentDate.AddMonths(-1);
            SelectedDateChanged.InvokeAsync(currentDate);
            UpdateDays();
        }

        private void NextMonth()
        {
            currentDate = currentDate.AddMonths(1);
            SelectedDateChanged.InvokeAsync(currentDate);
            UpdateDays();
        }

        private string GetDayClasses(DateTime day)
        {
            if (day == DateTime.Today)
            {
                return "w-current-day";
            }
            return "";
        }

        protected void SetDate(DateTime? date, bool updateValue)
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
                    return;
                }
                _value = date;
            }
        }

        /// <summary>
        /// Get week names 
        /// </summary>
        /// <returns>return Mo, Tu, We, Th, Fr, Sa, Su in the right culture</returns>
        protected IEnumerable<string> GetAbbreviatedDayNames()
        {
            var dayNamesNormal = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
            var dayNamesShifted = Shift(dayNamesNormal, (int)GetFirstDayOfWeek());
            return dayNamesShifted;
        }

        protected DayOfWeek GetFirstDayOfWeek()
        {
            if (FirstDayOfWeek.HasValue)
                return FirstDayOfWeek.Value;
            return CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        }
        /// <summary>
        /// Shift array and cycle around from the end
        /// </summary>
        private static T[] Shift<T>(T[] array, int positions)
        {
            var copy = new T[array.Length];
            Array.Copy(array, 0, copy, array.Length - positions, positions);
            Array.Copy(array, positions, copy, 0, array.Length - positions);
            return copy;
        }
    }
}
