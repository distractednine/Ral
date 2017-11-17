using System.Linq;
using System.Web.Configuration;
using RAL.Models.Interfaces;
using RAL_DAL;

namespace RAL.Models
{
    public class EfRalRepository : IRalRepository
    {
        private readonly RalDbContext context;

        public EfRalRepository()
        {
            context = new RalDbContext(WebConfigurationManager.AppSettings["RalConnString"]);
        }

        public IQueryable<Anime> anime
        {
            get { return context.anime; }
        }

        public IQueryable<User> users
        {
            get { return context.users; }
        }

        public IQueryable<Plan> plans
        {
            get { return context.plans; }
        }

        public IQueryable<Watcheda> watcheda
        {
            get { return context.watcheda; }
        }

        public void addUser(UserProfileViewModel u)
        {
            var newUser = new User
            {
                login = u.login,
                password = u.password,
                email = u.email,
                regtime = u.regDate.ToString()
            };

            context.users.Add(newUser);
            context.SaveChanges();
        }

        public void addWatcheda(int userId, WatchedAViewModel wavm)
        {
            var a = new Anime
            {
                name = wavm.name,
                year = wavm.year,
                type = wavm.type,
                seasonnum = wavm.seasonnum,
                epcount = wavm.epcount,
                waid = wavm.waid
            };

            var wa = new Watcheda
            {
                anime = a,
                startdate = wavm.startdate,
                finishdate = wavm.finishdate,
                rating = wavm.rating,
                watchedtime = wavm.watchedtime,
                adddate = wavm.adddate,
                status = wavm.status,
                epdropped = wavm.epdropped
            };

            context.anime.Add(a);
            context.users.AsEnumerable().First(u => u.id == userId).watcheda.Add(wa);

            context.SaveChanges();
        }

        public void deleteWatcheda(int userId, int watchedaId)
        {
            var a2del = context.users.AsEnumerable().First(u => u.id == userId).
                watcheda.AsEnumerable().First(wa => wa.id == watchedaId).anime;

            var wa2del = context.users.AsEnumerable().First(u => u.id == userId).
                watcheda.AsEnumerable().First(wa => wa.id == watchedaId);

            context.anime.Remove(a2del);
            context.watcheda.Remove(wa2del);
            context.users.AsEnumerable().First(u => u.id == userId).watcheda.Remove(wa2del);

            context.SaveChanges();
        }
    }
}