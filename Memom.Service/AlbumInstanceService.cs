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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlbumInstanceService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlbumInstanceService.svc or AlbumInstanceService.svc.cs at the Solution Explorer and start debugging.
    public class AlbumInstanceService : Service<UserAlbumInstance>, IAlbumInstanceService
    {
        /// <summary>
        ///     All methods that are exposed from Repository in Service are overridable to add business logic,
        ///     business logic should be in the Service layer and not in repository for separation of concerns.
        /// </summary>

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryAsync<UserAlbumInstance> _repositoryAlbumInst;
        private readonly IRepositoryAsync<Album> _repositoryAlbum;
        //private readonly IRepositoryAsync<UserAccount> _repositoryUser;
        private readonly IAppDbStoredProcedures _storedProcedures;

        public AlbumInstanceService(IRepositoryAsync<UserAlbumInstance> repositoryAlbumInst,  IRepositoryAsync<Album> repositoryAlbum, IUnitOfWorkAsync unitOfWork, IAppDbStoredProcedures storedProcedures)
            : base(repositoryAlbumInst)
        {
            //_repositoryUser = repositoryUser;
            _repositoryAlbum = repositoryAlbum;
            _repositoryAlbumInst = repositoryAlbumInst;
            _storedProcedures = storedProcedures;
            _unitOfWork = unitOfWork;
        }

        #region AlbumMaster
        public decimal AlbumInstancesTotal()
        {
            // add business logic here
            return _repositoryAlbumInst.AlbumInstancesTotal();
        }



        public IPagedList<UserAlbumInstance> AlbumInstancesByAlbumKey(int Key, int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryAlbumInst.AlbumInstancesByAlbumKey(_repositoryAlbum, Key).ToPagedList<UserAlbumInstance>(pageNumber, pageSize);

        }


        public IPagedList<UserAlbumInstance> AlbumInstancesAll(int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryAlbumInst.AlbumInstancesAll().ToPagedList<UserAlbumInstance>(pageNumber, pageSize);
        }

        public override void Insert(UserAlbumInstance entity)
        {
            // e.g. add business logic here before inserting
            _repositoryAlbumInst.Insert(entity);
            _unitOfWork.SaveChanges();
        }


        public void Insert(UserAlbumInstance entity, int UserKey)
        {
            // e.g. add business logic here before inserting
            _repositoryAlbumInst.Insert(entity);
            _unitOfWork.SaveChanges();
            _storedProcedures.PhotoAddTagProcessing(UserKey, entity.Key);
            _unitOfWork.SaveChanges();
        }

        public override void Delete(object id)
        {
            // e.g. add business logic here before deleting
            base.Delete(id);
            _unitOfWork.SaveChanges();
        }


        public override void Update(UserAlbumInstance entity)
        {
            // e.g. add business logic here before inserting
            base.Update(entity);
            _unitOfWork.SaveChanges();

        }

        public UserAlbumInstance FindAlbumInstance(int id)
        {
            return base.Find(id);
        }


        #endregion


    }


}