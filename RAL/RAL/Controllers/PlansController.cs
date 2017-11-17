using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAL_DAL;
using RAL.Models;
using RAL.Models.Interfaces;
using RAL.Infrastructure;
using RAL.Helpers;
using RAL.Helpers.Comparators;

namespace RAL.Controllers
{
    [Authorize]
    public class PlansController : Controller
    {
        IRalRepository repository;

        public PlansController(IRalRepository _repository)
        {
            repository = _repository;
        }

        public ActionResult Index(User user)
        {
            List<Watcheda> currentlist = repository.users.AsEnumerable().First(u => u.id == user.id).watcheda.ToList<Watcheda>();
            currentlist.Sort(new StartDateComparer());

            return View(new AHistoryViewModel { curUser = user, curList = currentlist });
        }
	}
}