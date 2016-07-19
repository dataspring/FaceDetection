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

    public interface IMemberService : IService<Member>
    {

        #region MemberMaster


        decimal MembersTotal();

        IPagedList<Member> MembersByName(string gameName, int pageNumber = 1, int pageSize = 15);

        IPagedList<Member> MembersByEmail(string Email, int pageNumber = 1, int pageSize = 15);


        IPagedList<Member> MembersAll(int pageNumber = 1, int pageSize = 15);

        void Insert(Member entity);
        
        void Delete(object id);

        void Update(Member entity);

        int UpdateMemberAddFace(Member entity, string Face);

        int UpdateMemberReplaceFace(Member entity, string Face, string OldFace);

        Member FindMember(int id);

        Member FindMember(string Email, int id);

        MemberDetails FindMemberDetails(string Email, int id);

        #endregion

        #region Dashboard

        
        
        IEnumerable<Scores> DashboardScores(string Email);
       
       
        #endregion

    }

}
