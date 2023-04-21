using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermesVideoRental.Components
{
    public partial class Visit
    {
        public string DayOfWeekOfVisit
        {
            get
            {
                var culture = new System.Globalization.CultureInfo("ru");
                var result =culture.DateTimeFormat.GetDayName(Date.DayOfWeek);
                result = culture.TextInfo.ToTitleCase(result.ToLower());
                return result;
            }
        }

        public string TimeOfVisit => Date.TimeOfDay.ToString();
    }
}
