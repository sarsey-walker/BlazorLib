using BlazorLib.Enums;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace BlazorLib.Components.DatePicker
{
    public partial class CustomCalendar : WBaseDate
    {
        [Parameter] public bool Fill { get; set; } = false;
        private DateTime currentDate = DateTime.Today;
        private DateTime currentMonth => new(currentDate.Year, currentDate.Month, 1);
        private int currentYear => currentMonth.Year;
        private int daysInMonth => DateTime.DaysInMonth(currentYear, currentMonth.Month);

        private List<DateTime> days = new List<DateTime>();
        private string CalendarClass()
        {
            return Fill ? "calendar-border-fill" : "calendar-border-fit";
        }
        private SwipeDirection _swipeDirection = SwipeDirection.None;

        private void OnSwiped(SwipeDirection swipeDirection)
        {
            _swipeDirection = swipeDirection;

            if (_swipeDirection == SwipeDirection.RightToLeft)
            {
                NextMonth();
            }
            else if(_swipeDirection == SwipeDirection.LeftToRight)
            {
                PreviousMonth();
            }
        }
        protected override void OnInitialized()
        {
            if (Date == null)
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


        private void SelectDate(int index)
        {
            Date = days[index];
        }

        private void PreviousMonth()
        {
            currentDate = currentDate.AddMonths(-1);
            MonthChanged.InvokeAsync(currentDate);
            UpdateDays();
        }

        private void NextMonth()
        {
            currentDate = currentDate.AddMonths(1);
            MonthChanged.InvokeAsync(currentDate);
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
