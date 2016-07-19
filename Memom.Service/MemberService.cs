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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MemberService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MemberService.svc or MemberService.svc.cs at the Solution Explorer and start debugging.
    public class MemberService : Service<Member>, IMemberService
    {
        /// <summary>
        ///     All methods that are exposed from Repository in Service are overridable to add business logic,
        ///     business logic should be in the Service layer and not in repository for separation of concerns.
        /// </summary>

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryAsync<Member> _repositoryMember;
        private readonly IRepositoryAsync<UserAccount> _repositoryUser;
        private readonly IAppDbStoredProcedures _storedProcedures;

        public MemberService(IRepositoryAsync<Member> repositoryMember, IRepositoryAsync<UserAccount> repositoryUser, IUnitOfWorkAsync unitOfWork, IAppDbStoredProcedures storedProcedures)
            : base(repositoryMember)
        {
            _repositoryUser = repositoryUser;
            _repositoryMember = repositoryMember;
            _storedProcedures = storedProcedures;
            _unitOfWork = unitOfWork;
        }

        #region MemberMaster
        public decimal MembersTotal()
        {
            // add business logic here
            return _repositoryMember.MembersTotal();
        }

        public IPagedList<Member> MembersByEmail(string Email, int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryMember.MembersByEmail(_repositoryUser, Email).ToPagedList<Member>(pageNumber, pageSize);
        }

        public IPagedList<Member> MembersByName(string gameName, int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryMember.MembersByName(gameName).ToPagedList<Member>(pageNumber, pageSize);

        }


        public IPagedList<Member> MembersAll(int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryMember.MembersAll().ToPagedList<Member>(pageNumber, pageSize);;
        }

        public override void Insert(Member entity)
        {
            // e.g. add business logic here before inserting
            _repositoryMember.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public override void Delete(object id)
        {
            // e.g. add business logic here before deleting
            base.Delete(id);
            _unitOfWork.SaveChanges();
        }


        public override void Update(Member entity)
        {
            // e.g. add business logic here before inserting
            base.Update(entity);
            _unitOfWork.SaveChanges();

        }

        public Member FindMember(int id)
        {
            return base.Find(id);
        }

        public Member FindMember(string Email, int id)
        {
            return _repositoryMember.FindMemberByEmail(_repositoryUser, Email, id);
        }

        public MemberDetails FindMemberDetails(string Email, int id)
        {
            //return _repositoryMember.FindMemberByEmail(_repositoryUser, Email, id);
            return _storedProcedures.MemberViewDetails(Email, id);
        }


        #endregion


        #region Score


        public IEnumerable<Scores> DashboardScores(string Email)
        {
            return _storedProcedures.DashboardScores(Email).Take(4);
        }




        #endregion




        public int UpdateMemberAddFace(Member entity, string Face)
        {
            // e.g. add business logic here before inserting
            base.Update(entity);
            _unitOfWork.SaveChanges();
            _storedProcedures.FaceAddTagProcessing(entity.UserKey, entity.Key, Face);
            return _unitOfWork.SaveChanges();

        }


        public int UpdateMemberReplaceFace(Member entity, string Face, string OldFace)
        {
            base.Update(entity);
             _unitOfWork.SaveChanges();
            _storedProcedures.FaceReplaceTagProcessing (entity.UserKey, entity.Key, Face, OldFace);
            return _unitOfWork.SaveChanges();
        }
    }


}