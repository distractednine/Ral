using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAL_DAL;

namespace RAL.Models.Interfaces
{
    public interface IRalRepository
    {
        IQueryable<Anime> anime { get; }
        IQueryable<Plan> plans { get; }
        IQueryable<User> users { get; }
        IQueryable<Watcheda> watcheda { get; }

        void addUser(UserProfileViewModel u);
        void addWatcheda(int userId, WatchedAViewModel wavm);
        void deleteWatcheda(int userId, int watchedaId);
    }
}