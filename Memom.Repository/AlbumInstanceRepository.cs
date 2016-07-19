
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Memom.Entities.Models;
using Repository.Pattern.Repositories;


namespace Memom.Repo.Repositories
{
    // Exmaple: How to add custom methods to a repository.
    public static class AlbumInstanceRepository
    {
        public static decimal AlbumInstancesTotal(this IRepository<UserAlbumInstance> repository)
        {
            return repository.Queryable().Count();
        }


        public static IEnumerable<UserAlbumInstance> AlbumInstancesByAlbumKey(this IRepository<UserAlbumInstance> repository, IRepository<Album> userAlbum, int albumKey)
        {
            return (from uai in repository.Queryable()
                    join ua in userAlbum.Queryable()
                    on uai.AlbumKey equals ua.Key
                    where ua.Key.Equals(albumKey)
                    select uai).ToList();
        }


        public static IEnumerable<UserAlbumInstance> AlbumInstancesAll(this IRepository<UserAlbumInstance> repository)
        {
            return repository
                .Queryable()
                .ToList();
        }

       
    }
}