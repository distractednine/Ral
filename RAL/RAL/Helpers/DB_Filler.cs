using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAL_DAL;
using RAL.Models;
using RAL.Models.Interfaces;
using System.Globalization;

namespace RAL.Helpers
{
    public class DB_Filler
    {
        Random random;
        IRalRepository repository;
        int userId;

        public DB_Filler(IRalRepository _repository, int _userId)
        {
            random = new Random();
            repository = _repository;
            userId = _userId;
        }

        public void fillUserHistory(List<WatchedAViewModel> _list)
        {
            foreach(WatchedAViewModel wavm in _list)
            {
                repository.addWatcheda(userId, wavm);
            }
        }

        public List<WatchedAViewModel> getWatchedAList(int count)
        {
            List<WatchedAViewModel> walist = new List<WatchedAViewModel>();

            for (int i = 1; i <= count; i++)
            {
                walist.Add(createWAVM(i));
            }

            return walist;
        }

        WatchedAViewModel createWAVM(int i)
        {
            WatchedAViewModel wa = new WatchedAViewModel()
            {
                name = getName(i),
                year = getYear(),
                type = getType(),
                seasonnum = 1,
                waid = getWaId(),
                startdate = getStartDate(),
                rating = getRating(),
                watchedtime = 1,
                adddate = DateTime.Now.ToString(),
                status = "Finished"
            };

            wa.epcount = getEpCount(wa.type);
            wa.finishdate = getFinishdate(wa.startdate);

            return wa;
        }

        
        string getName(int i)
        {
            return "aname" + i.ToString();
        }

        string getYear()
        {
            return random.Next(1950, 2049).ToString();
        }

        string getType()
        {
            int choice = random.Next(1, 4);

            switch (choice)
            {
                case (1): return "TV";
                case (2): return "OVA";
                case (3): return "Movie";
                default: return "TV";
            }
        }

        int? getEpCount(string type)
        {
            if (type != "Movie")
            {
                return random.Next(1, 53);
            }
            else
            {
                return null;
            }
        }

        int? getWaId()
        {
            return random.Next(1, 3000);
        }

        string getStartDate()
        {
            string day = appendZero(random.Next(1, 31).ToString());

            string month = appendZero(random.Next(1, 13).ToString());

            string year = getYear();

            return day + '/' + month + '/' + year + " 00:00";
        }

        string appendZero(string value)
        {
            return value.Length == 1 ? value = '0' + value : value;
        }

        string getFinishdate(string startDate)
        {
            DateTime date;
            if (DateTime.TryParse(startDate, new CultureInfo("ru-RU"), DateTimeStyles.None, out date))
            {
                date = date.AddMonths(random.Next(1, 6)).AddDays(random.Next(1, 25));
            }
            else
            {
                return "";
            }

            return appendZero(date.Day.ToString()) + '/' + appendZero(date.Month.ToString()) + '/' + date.Year.ToString() + " 00:00";
        }

        double getRating()
        {
            return Math.Round(random.NextDouble() * 10, 2);
        }
    }
}