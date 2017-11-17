using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using RAL_DAL;

namespace RAL.Helpers
{
    public class WorldArtParser
    {
        static string nameYearPattern
        {
            get
            {
                return @"(font size=3 color=#990000>(1|2)(9|0)[0-9][0-9]<\/font><\/a><font size=3>]<\/b><\/font><br>.{3,}?(<br|<\/td))";
            }
        }

        static string typeEpcountPattern
        {
            get
            {
                return @"(<b>Тип<\/b>:.(((OVA|ТВ|ТВ-спэшл|ONA).(\(| ).([0-9][0-9][0-9][0-9])?.)|(полнометражный фильм|короткометражный фильм|музыкальное видео)))";
            }
        }

        string nameYearHtml;
        string typeEpcountHtml;
        public Anime targetAnime;

        public WorldArtParser(int animeId)
        {
            string worldArtHtmlStr = getHtmlFromWorldArt(animeId);

            if (worldArtHtmlStr.Contains("//www.world-art.ru/not_connect.html"))
            {
                targetAnime = null;
            }
            else
            {
                nameYearHtml = getPatternMatch(worldArtHtmlStr, nameYearPattern);
                typeEpcountHtml = getPatternMatch(worldArtHtmlStr, typeEpcountPattern);

                if (nameYearHtml == "" || typeEpcountHtml == "")
                {
                    targetAnime = null;
                }
                else
                {
                    targetAnime = createAInfo();
                }
            }
        }

        public Anime createAInfo()
        {
            Anime a = new Anime();

            try
            {
                a.name = getName();
                a.year = getYear();
                a.type = getType();

                if (a.type == "TV" || a.type == "OVA")
                {
                    a.epcount = getEpcount();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return a;
        }

        string getHtmlFromWorldArt(int worldartID)
        {
            string WorldArtHtmlstr;

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                string requestUrl = string.Concat("http://www.world-art.ru/animation/animation.php?id=", worldartID);
                WorldArtHtmlstr = client.DownloadString(requestUrl);
            }

            return WorldArtHtmlstr;
        }

        string getPatternMatch(string targetStr, string pattern)
        {
            string result = string.Empty;

            foreach (Match match in Regex.Matches(targetStr, pattern, RegexOptions.None))
            {
                result += match.Value;
            }

            return result;
        }

        string getName()
        {
            int begin = nameYearHtml.LastIndexOf('>') + 1;
            int end = nameYearHtml.LastIndexOf('<');

            string aname = nameYearHtml.Substring(begin, end - begin);

            if (aname.Contains("&#x27;"))
            {
                aname = aname.Replace("&#x27;", "'");
            }

            return aname;
        }

        string getYear()
        {
            int begin = nameYearHtml.IndexOf('>') + 1;
            int end = nameYearHtml.IndexOf('<');
            return nameYearHtml.Substring(begin, end - begin);
        }

        string getType()
        {
            if (typeEpcountHtml.Contains("OVA") || typeEpcountHtml.Contains("ONA") || typeEpcountHtml.Contains("ТВ-спэшл"))
            {
                return "OVA";
            }
            else if (typeEpcountHtml.Contains("ТВ"))
            {
                return "TV";
            }
            else if (typeEpcountHtml.Contains("фильм"))
            {
                return "Movie";
            }
            else
            {
                return null;
            }
        }

        int? getEpcount()
        {
            int? epc;

            int[] arr = { typeEpcountHtml.LastIndexOf('('), typeEpcountHtml.LastIndexOf('<'), typeEpcountHtml.LastIndexOf('>') };
            int begin = arr.Max();
            begin++;
            int end = typeEpcountHtml.Length;

            try 
            {
                epc = int.Parse(typeEpcountHtml.Substring(begin, end - begin));
            }
            catch(Exception)
            {
                return null;
            }

            return epc;
        }
    }
}