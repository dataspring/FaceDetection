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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAlbumService" in both code and config file together.

    public interface IAlbumService : IService<Album>
    {

        #region AlbumMaster

        
        decimal AlbumsTotal();

        Album FindAlbumsInEmail(string Email, int AlbumKey);

        IPagedList<Album> AlbumsByEmail(string Email, int pageNumber = 1, int pageSize = 15);


        IPagedList<Album> AlbumsByName(string gameName, int pageNumber = 1, int pageSize = 15);
        
        IPagedList<Album> AlbumsByType(string gameType, int pageNumber = 1, int pageSize = 15);


        IPagedList<Album> AlbumsAll(int pageNumber = 1, int pageSize = 15);

        void Insert(Album entity);
        
        void Delete(object id);
        
        void Update(Album entity);
        
        Album FindAlbum(int id);

        #endregion

        #region Score

        
        UpdateAlbumDownloadResult UpdateDownload(string Email, string AlbumName, string DownloadDateTime, string remoteAddress);

        
        IEnumerable<Scores> DashboardScores(string Email);

        IPagedList<AlbumScores> AlbumDashboardScores(string Email, int pageNumber = 1, int pageSize = 15);

        IPagedList<FaceViewAlbumInstance> ViewFaceMemberPhotos(int MemberKey, int? AlbumKey, int pageNumber = 1, int pageSize = 15);
         
        
        #endregion

    }



}
