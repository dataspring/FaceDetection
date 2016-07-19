using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


using Memom.Entities.Models;
using Memom.Repo.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;
using Repository.Pattern.UnitOfWork;
using PagedList;


namespace Memom.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlbumService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlbumService.svc or AlbumService.svc.cs at the Solution Explorer and start debugging.
    public class AlbumService : Service<Album>, IAlbumService
    {
        /// <summary>
        ///     All methods that are exposed from Repository in Service are overridable to add business logic,
        ///     business logic should be in the Service layer and not in repository for separation of concerns.
        /// </summary>

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryAsync<Album> _repositoryAlbum;
        private readonly IRepositoryAsync<UserAccount> _repositoryUser;
        private readonly IAppDbStoredProcedures _storedProcedures;

        public AlbumService(IRepositoryAsync<Album> repositoryAlbum, IRepositoryAsync<UserAccount> repositoryUser, IUnitOfWorkAsync unitOfWork, IAppDbStoredProcedures storedProcedures)
            : base(repositoryAlbum)
        {
            _repositoryUser = repositoryUser;
            _repositoryAlbum = repositoryAlbum;
            _storedProcedures = storedProcedures;
            _unitOfWork = unitOfWork;
        }

        #region AlbumMaster
        public decimal AlbumsTotal()
        {
            // add business logic here
            return _repositoryAlbum.AlbumsTotal();
        }

        public Album FindAlbumsInEmail(string Email, int AlbumKey)
        {
            return _repositoryAlbum.FindAlbumsInEmail(_repositoryUser, Email, AlbumKey);
        }
       

        public IPagedList<Album> AlbumsByEmail(string Email, int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryAlbum.AlbumsByEmail(_repositoryUser, Email).ToPagedList<Album>(pageNumber, pageSize);
        }

        public IPagedList<Album> AlbumsByType(string gameType, int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryAlbum.AlbumsByType(gameType).ToPagedList<Album>(pageNumber, pageSize);
        }

        public IPagedList<Album> AlbumsByName(string gameName, int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryAlbum.AlbumsByName(gameName).ToPagedList<Album>(pageNumber, pageSize);

        }


        public IPagedList<Album> AlbumsAll(int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryAlbum.AlbumsAll().ToPagedList<Album>(pageNumber, pageSize);
        }

        public override void Insert(Album entity)
        {
            // e.g. add business logic here before inserting
            _repositoryAlbum.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public override void Delete(object id)
        {
            // e.g. add business logic here before deleting
            base.Delete(id);
            _unitOfWork.SaveChanges();
        }


        public override void Update(Album entity)
        {
            // e.g. add business logic here before inserting
            base.Update(entity);
            _unitOfWork.SaveChanges();

        }

        public Album FindAlbum(int id)
        {
            return base.Find(id);
        }


        #endregion


        #region Score

        public UpdateAlbumDownloadResult UpdateDownload(string Email, string AlbumName, string DownloadDateTime, string remoteAddress)
        {
            return _storedProcedures.UpdateDownload(Email, AlbumName, DownloadDateTime, remoteAddress).First(); ;
        }

        public IEnumerable<Scores> DashboardScores(string Email)
        {
            return _storedProcedures.DashboardScores(Email);
        }

        public IPagedList<AlbumScores> AlbumDashboardScores(string Email, int pageNumber, int pageSize)
        {
            return _storedProcedures.AlbumDashboardScores(Email).ToPagedList<AlbumScores>(pageNumber, pageSize);
        }






        #endregion




        public IPagedList<FaceViewAlbumInstance> ViewFaceMemberPhotos(int MemberKey, int? AlbumKey, int pageNumber, int pageSize)
        {
            return _storedProcedures.ViewFaceMemberPhotos(MemberKey, AlbumKey).ToPagedList<FaceViewAlbumInstance>(pageNumber, pageSize); 
        }
    }


}