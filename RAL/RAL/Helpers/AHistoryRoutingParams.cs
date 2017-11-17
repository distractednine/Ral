using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAL.Helpers
{
    public class AHistoryRoutingParams
    {
        public string marker, year;
        HttpRequestBase request;

        public AHistoryRoutingParams(HttpRequestBase _request, string _marker, string _year)
        {
            request = _request;

            if (_marker != null)
            {
                marker = _marker;
                year = _year;
            }
            else
            {
                marker = getAHistoryMarker();
                if (marker == "Year")
                {
                    year = getAHistoryMarkerYear();
                }
            }
        }

        string getAHistoryMarker()
        {
            string requestStr = getPath();
            const string defaultPath = "/AHistory";
            const string yearPath = "Year";

            if (requestStr == defaultPath || requestStr == defaultPath + "/All")
            {
                return "All";
            }
            else
            {
                string marker = requestStr.Substring(defaultPath.Length + 1);

                if (marker.Substring(0, 4) == yearPath)
                {
                    return yearPath;
                }
                else
                {
                    return marker;
                }
            }
        }

        string getAHistoryMarkerYear()
        {
            string year = request.Path.Substring(request.Path.Length - 4);

            try
            {
                int y = Int32.Parse(year);
            }
            catch (Exception)
            {
                return null;
            }
            return year;
        }

        string getPath()
        {
            string str2Rem = "AHistoryList/";
            string requestStr = request.Path;

            if(requestStr.Contains(str2Rem))
            {
                int index = requestStr.IndexOf(str2Rem);
                return requestStr.Remove(index, str2Rem.Length);
            }

            return requestStr;
        }
    }
}