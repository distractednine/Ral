using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using RAL_DAL;

namespace RAL.Helpers.Comparators
{
    public class StartDateComparer : IComparer<Watcheda>
    {
        DateTime parseDate(string val)
        {
            DateTime parsed;
            return DateTime.TryParse(val, new CultureInfo("ru-RU"), DateTimeStyles.None, out parsed) ? parsed : new DateTime();
        }

        public int Compare(Watcheda first, Watcheda second)
        {
            return DateTime.Compare(parseDate(first.startdate), parseDate(second.startdate));
        }
    }
}