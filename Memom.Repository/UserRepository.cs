using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Memom.Entities.Models;
using Repository.Pattern.Repositories;


namespace Memom.Repo.Repositories
{
    public static class UserRepository
    {
        public static UserAccount UserDetails(this IRepository<UserAccount> repository, string Email)
        {
            return repository
                .Queryable()
                .Where(c => c.Username == Email)
                //.Include(g => g.UserAlbums.Select(k => k.Album))
                //.Include(r => r.UserRewards.Select(a => a.AlbumRewardAchievement))
                //.Include(i => i.UserAlbumInstances.Select(j => j.Album))
                 .First();
                // take first 20 of latest game instances and 20 of user rewards
            //return repository
            //        .Queryable()
            //        .Where(c => c.Username == Email)
            //        .Include(g => g.UserAlbums.Select(k => k.Album))
            //        .Include(r => r.UserRewards.Select(a => a.AlbumRewardAchievement).Take(20).OrderByDescending(o => o.Created))
            //        .Include(i => i.UserAlbumInstances.Select(j => j.Album).Take(20).OrderByDescending(o => o.Created))
            //         .First();
               
        }

        public static decimal UsersTotal(this IRepository<UserAccount> repository)
        {
            return repository.Queryable().Count();
        }

        public static IEnumerable<UserAccount> UsersByName(this IRepository<UserAccount> repository, string userName)
        {
            return repository
                .Queryable()
                .Where(c => c.DisplayName.Contains(userName))
                .OrderByDescending(c => c.Created)
                .ToList();
        }


        public static IEnumerable<UserAccount> UsersByEmail(this IRepository<UserAccount> repository, string userEmail)
        {
            return repository
                .Queryable()
                .Where(x => x.Email.Contains(userEmail))
                .OrderByDescending(c => c.Created)
                .ToList();
        }

        public static IEnumerable<UserAccount> UsersAll(this IRepository<UserAccount> repository)
        {
            return repository
                .Queryable()
                .OrderByDescending(c => c.Created)
                .ToList();
        }

    }
}
