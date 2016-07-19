
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Memom.Entities.Models;
using Repository.Pattern.Repositories;


namespace Memom.Repo.Repositories
{
    // Exmaple: How to add custom methods to a repository.
    public static class AlbumRepository
    {
        public static decimal AlbumsTotal(this IRepository<Album> repository)
        {
            return repository.Queryable().Count();
        }


        public static IEnumerable<Album> AlbumsByEmail(this IRepository<Album> repository, IRepository<UserAccount> userAccount, string Email)
        {
            return userAccount.Queryable()
                        .Where(usr => usr.Email.Equals(Email))
                        .Join(repository.Queryable(), usr => usr.Key, mem => mem.UserKey, (usr, mem) => mem).Where(m => m.IsAttached == true)
                        .ToList();
        }

        public static Album FindAlbumsInEmail(this IRepository<Album> repository, IRepository<UserAccount> userAccount, string Email, int AlbumKey)
        {
            return userAccount.Queryable()
                        .Where(usr => usr.Email.Equals(Email))
                        .Join(repository.Queryable(), usr => usr.Key, mem => mem.UserKey, (usr, mem) => mem).Where(m => m.IsAttached == true && m.Key == AlbumKey)
                        .FirstOrDefault();
        }

        public static IEnumerable<Album> AlbumsByName(this IRepository<Album> repository, string gameName)
        {
            return repository
                .Queryable()
                .Where(c => c.Name.Contains(gameName))
                .ToList();
        }


        public static IEnumerable<Album> AlbumsByType(this IRepository<Album> repository, string gameType)
        {
            return repository
                .Queryable()
                //.Where(x => x.UserKey.Contains(gameType))
                .ToList();
        }

        public static IEnumerable<Album> AlbumsAll(this IRepository<Album> repository)
        {
            return repository
                .Queryable()
                .ToList();
        }


       
    }
}