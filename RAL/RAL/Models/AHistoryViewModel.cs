using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAL_DAL;

namespace RAL.Models
{
    //2del
    public class AHistoryViewModel
    {
        public User curUser { get; set; }
        public List<Watcheda> curList { get; set; }
    }
}