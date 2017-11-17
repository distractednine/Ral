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
    public class AHistoryController : Controller
    {
        IRalRepository repository;

        public AHistoryController(IRalRepository _repository)
        {
            repository = _repository;
        }

        public ActionResult Start()
        {
            return RedirectToAction("Index");
        }

        [Route("AHistory/{marker?}/{year?}")]
        public ActionResult Index(User user)
        {
            if (user == null)
            {
                return RedirectToAction("LogOn", "Account");
            }

            return View(user);
        }

        [Route("AHistory/AHistoryList/{marker?}/{year?}")]
        public ActionResult AHistoryList(User user, string _marker = null, string _year = null)
        {
            AHistoryRoutingParams rp = new AHistoryRoutingParams(Request, _marker, _year);

            List<Watcheda> currentlist = repository.users.AsEnumerable().First(u => u.id == user.id).watcheda.ToList<Watcheda>();

            try
            {
                currentlist = AHistoryListGenerator.generateList(currentlist, rp.marker, rp.year);
                currentlist.Sort(new StartDateComparer());
            }
            catch
            {
                //return View("InvalidURL", user);
                return View("AHistoryList", null);
            }
            return View(currentlist);
        }

        [Route("AHistory/AddWatcheda")]
        public ActionResult AddWatcheda(User user, WatchedAViewModel watcheda)
        {
            if (!ModelState.IsValid)
            {
                return Json((object)getWatchedaModelErrors(watcheda), JsonRequestBehavior.AllowGet);
            }

            repository.addWatcheda(user.id, watcheda);

            return Json((object)string.Empty, JsonRequestBehavior.AllowGet);
        }

        [Route("AHistory/AHistoryTableRow")]
        public ActionResult AHistoryTableRow(User user, string a2get_name, string a2get_startdate)
        {
            Watcheda wa = repository.users.AsEnumerable().First(u => u.id == user.id).watcheda.ToList<Watcheda>().
                                     First(wtchd => wtchd.anime.name == a2get_name && wtchd.startdate == a2get_startdate);

            if (wa == null)
            {
                return Json((object)"error", JsonRequestBehavior.AllowGet);
            }

            return View("AHistoryTableRow",wa);
        }

        [Route("AHistory/DeleteWatcheda")]
        public JsonResult DeleteWatcheda(User user, int a2del_id)
        {
            string result = "success";
            try
            {
                repository.deleteWatcheda(user.id, a2del_id);
                //deleteAllWatchedaWithSameName(user, a2del_id);
            }
            catch (Exception)
            {
                result = "error";
            }
            return Json((object)result, JsonRequestBehavior.AllowGet);
        }

        [Route("AHistory/GetInfoFromWA")]
        public ActionResult GetInfoFromWA(int worldartID)
        {
            WorldArtParser wap = new WorldArtParser(worldartID);
            if (wap.targetAnime != null)
            {
                return Json((object)wap.targetAnime, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json((object)"error", JsonRequestBehavior.AllowGet);
            }
        }

        [Route("InvalidURL")]
        public ActionResult InvalidURL(User user)
        {
            return View("InvalidURL", user);
        }

        [Route("AHistory/setNewWatcheda")]
        public void setNewWatcheda(User user, int? id)
        {
            Watcheda newWatcheda = repository.users.AsEnumerable().First(u => u.id == user.id).watcheda.ToList<Watcheda>().
                                     First(wtchd => wtchd.id == id);

            TempData["newWatcheda"] = newWatcheda;
        }

        [NonAction]
        string getWatchedaModelErrors(WatchedAViewModel watcheda)
        {
            return string.Join(" ",
                    ModelState.Values.SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());
        }

        //test - 2del
        [NonAction]
        void deleteAllWatchedaWithSameName(User user, int a2del_id)
        {
            string aId2delName = repository.users.AsEnumerable().First(u => u.id == user.id).watcheda.ToList<Watcheda>().
                                     First(wtchd => wtchd.id == a2del_id).anime.name;

            int[] delList = repository.users.AsEnumerable().First(u => u.id == user.id).watcheda.ToList<Watcheda>().
                                     Where(wtchd => wtchd.anime.name == aId2delName).Select(wa => wa.id).ToArray<int>();
            
            for (int i = 0; i < delList.Count(); i++)
            {
                repository.deleteWatcheda(user.id, delList[i]);
            }
        }
    }
}