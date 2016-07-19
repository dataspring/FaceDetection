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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserAlbumInstanceDetailService" in both code and config file together.

    public interface IAlbumInstanceDetailService : IService<UserAlbumInstanceDetail>
    {

        #region AlbumInstanceDetailMaster

        void Insert(UserAlbumInstanceDetail entity);

        void Delete(object id);
        
        void Update(UserAlbumInstanceDetail entity);
        
        UserAlbumInstanceDetail FindAlbumInstanceDetail(int id);

        IEnumerable<UserAlbumInstanceDetail> GetPhotosForBatchProcssing();

        #endregion

  

    }



}
