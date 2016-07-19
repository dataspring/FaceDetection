using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Memom.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using PagedList;



namespace Memom.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserAlbumInstanceService" in both code and config file together.

    public interface IAlbumInstanceService : IService<UserAlbumInstance>
    {

        #region AlbumInstanceMaster

        
        decimal AlbumInstancesTotal();

        IPagedList<UserAlbumInstance> AlbumInstancesByAlbumKey(int Key, int pageNumber = 1, int pageSize = 15);


        IPagedList<UserAlbumInstance> AlbumInstancesAll(int pageNumber = 1, int pageSize = 15);

        void Insert(UserAlbumInstance entity);

        void Insert(UserAlbumInstance entity, int UserKey);
        
        void Delete(object id);
        
        void Update(UserAlbumInstance entity);
        
        UserAlbumInstance FindAlbumInstance(int id);

        #endregion

  

    }



}
