using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;

namespace RAL.Models
{
    [DatetimeValidator]
    [EpCountValidator]
    [RatingValidatorAttribute]
    public class WatchedAViewModel
    {
        //a
        [Required(ErrorMessage = "Please enter anime name ")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please enter year ")]
        [Range(1900, 2050, ErrorMessage = "Please enter correct year ")]
        public string year { get; set; }

        [Required(ErrorMessage = "Please enter anime type (TV/OVA/Movie) ")]
        public string type { get; set; }

        [Required(ErrorMessage = "Please enter TV/OVA/Movie number")]
        [Range(1, 999, ErrorMessage = "Please enter correct TV/OVA/Movie number ")]
        public Nullable<int> seasonnum { get; set; }

        [Range(1, 9999, ErrorMessage = "Please enter correct anime episode count ")]
        public Nullable<int> epcount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter correct anime World Art id ")]
        public Nullable<int> waid { get; set; }

        //wa
        [Required(ErrorMessage = "Please enter the date when you started watching this anime ")]
        public string startdate { get; set; }

        public string finishdate { get; set; }
        
        public Nullable<double> rating;
        string _rtng;
        public string rtng
        {
            get
            {
                return _rtng;
            }
            set
            {
                _rtng = value;

                if (_rtng != string.Empty && _rtng != null)
                {
                    try
                    {
                        // truncate to 2 numbers after comma
                        if (rtng.IndexOf('.') > -1 && rtng.Length>3)
                        {
                            _rtng = rtng.Substring(0, rtng.IndexOf('.') + 3);
                        }

                        rating = double.Parse(rtng, CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        rating = -1;
                    }
                }
            }
        }

        [Range(1, 9999, ErrorMessage = "Please enter correct anime watched time ")]
        public Nullable<int> watchedtime { get; set; }

        [Range(1, 9999, ErrorMessage = "Please enter correct episode on which you dropped this anime ")]
        public Nullable<int> epdropped { get; set; }

        public string adddate { get; set; }

        [Required(ErrorMessage = "Please enter anime status (current, finished, rewatched, dropped) ")]
        public string status { get; set; }
    }

    class DatetimeValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            WatchedAViewModel wa = value as WatchedAViewModel;
            DateTime dateValue1;
            DateTime dateValue2;

            if (!DateTime.TryParse(wa.startdate, new CultureInfo("ru-RU"), DateTimeStyles.None, out dateValue1))
            {
                ErrorMessage = "The first date/time value is incorrect ";
                return false;
            }

            if (wa.finishdate != null)
            {
                if (!DateTime.TryParse(wa.finishdate, new CultureInfo("ru-RU"), DateTimeStyles.None, out dateValue2))
                {
                    ErrorMessage = "The second date/time value is incorrect ";
                    return false;
                }
                else if (dateValue1 > dateValue2)
                {
                    ErrorMessage = "The second date/time value can't be earlier than the first one ";
                    return false;
                }
            }

            return true;
        }
    }

    class EpCountValidatorAttribute : ValidationAttribute
    {
        public EpCountValidatorAttribute()
        {
            ErrorMessage = "Please enter the number of episodes ";
        }

        public override bool IsValid(object value)
        {
            WatchedAViewModel wa = value as WatchedAViewModel;

            if (wa.type == "TV" || wa.type == "OVA")
            {
                return wa.epcount != null;
            }
            else
            {
                return true;
            }
        }
    }

    class RatingValidatorAttribute : ValidationAttribute
    {
        public RatingValidatorAttribute()
        {
            ErrorMessage = "Incorrect rating (values 0.0 - 10.0 are allowed) ";
        }

        public override bool IsValid(object value)
        {
            WatchedAViewModel wa = value as WatchedAViewModel;

            if (wa.rating != null)
            {
                return wa.rating >= 0 && wa.rating <= 10;
            }
            else
            {
                return true;
            }
        }
    }
}