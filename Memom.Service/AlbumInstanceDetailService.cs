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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlbumInstanceDetailService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlbumInstanceDetailService.svc or AlbumInstanceDetailService.svc.cs at the Solution Explorer and start debugging.
    public class AlbumInstanceDetailService : Service<UserAlbumInstanceDetail>, IAlbumInstanceDetailService
    {
        /// <summary>
        ///     All methods that are exposed from Repository in Service are overridable to add business logic,
        ///     business logic should be in the Service layer and not in repository for separation of concerns.
        /// </summary>

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryAsync<UserAlbumInstanceDetail> _repositoryAlbumInst;
        private readonly IRepositoryAsync<Album> _repositoryAlbum;
        //private readonly IRepositoryAsync<UserAccount> _repositoryUser;
        private readonly IAppDbStoredProcedures _storedProcedures;

        public AlbumInstanceDetailService(IRepositoryAsync<UserAlbumInstanceDetail> repositoryAlbumInst,  IRepositoryAsync<Album> repositoryAlbum, IUnitOfWorkAsync unitOfWork, IAppDbStoredProcedures storedProcedures)
            : base(repositoryAlbumInst)
        {
            //_repositoryUser = repositoryUser;
            _repositoryAlbum = repositoryAlbum;
            _repositoryAlbumInst = repositoryAlbumInst;
            _storedProcedures = storedProcedures;
            _unitOfWork = unitOfWork;
        }

        #region AlbumMaster



        public override void Insert(UserAlbumInstanceDetail entity)
        {
            // e.g. add business logic here before inserting
            _repositoryAlbumInst.Insert(entity);
            _unitOfWork.SaveChanges();
        }


        public override void Delete(object id)
        {
            // e.g. add business logic here before deleting
            base.Delete(id);
            _unitOfWork.SaveChanges();
        }


        public override void Update(UserAlbumInstanceDetail entity)
        {
            // e.g. add business logic here before inserting
            base.Update(entity);
            _unitOfWork.SaveChanges();

        }

        public UserAlbumInstanceDetail FindAlbumInstanceDetail(int id)
        {
            return base.Find(id);
        }

        public IEnumerable<UserAlbumInstanceDetail> GetPhotosForBatchProcssing()
        {
            return _storedProcedures.GetPhotosForBatchProcssing();
        }


        #endregion


    }


}