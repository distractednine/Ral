using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAL_DAL;

namespace RAL.Helpers
{
    public class AHistoryListGenerator
    {
        static public List<Watcheda> generateList(List<Watcheda> list, string marker, string year)
        {
            switch (marker)
            {
                case ("All"): return getAll(list);
                case ("Year"): return getByYear(list, year);
                case ("Finished"): return getFinished(list);
                case ("Current"): return getCurrent(list);
                case ("Rewatched"): return getRewatched(list);
                case ("Dropped"): return getDropped(list);
                default: throw new Exception();
            }
        }

        static List<Watcheda> getAll(List<Watcheda> list)
        {
            return list.Where(wa => wa.status != "Dropped").Select(wa => wa).ToList<Watcheda>();
        }

        static List<Watcheda> getByYear(List<Watcheda> list, string year)
        {
            if (year == null)
            {
                return null;
            }

            return list.Where(wa => wa.startdate.Substring(6, 4) == year && wa.status != "Dropped").Select(wa => wa).ToList<Watcheda>();
        }

        static List<Watcheda> getFinished(List<Watcheda> list)
        {
            return list.Where(wa => wa.status == "Finished").Select(wa => wa).ToList<Watcheda>();
        }

        static List<Watcheda> getCurrent(List<Watcheda> list)
        {
            return list.Where(wa => wa.status == "Current").Select(wa => wa).ToList<Watcheda>();
        }

        static List<Watcheda> getRewatched(List<Watcheda> list)
        {
            return list.Where(wa => wa.status == "Rewatched").Select(wa => wa).ToList<Watcheda>();
        }

        static List<Watcheda> getDropped(List<Watcheda> list)
        {
            return list.Where(wa => wa.status == "Dropped").Select(wa => wa).ToList<Watcheda>();
        }
    }
}