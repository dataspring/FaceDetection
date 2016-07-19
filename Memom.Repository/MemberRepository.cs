
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Memom.Entities.Models;
using Repository.Pattern.Repositories;


namespace Memom.Repo.Repositories
{
    // Exmaple: How to add custom methods to a repository.
    public static class MemberRepository
    {
        public static decimal MembersTotal(this IRepository<Member> repository)
        {
            return repository.Queryable().Count();
        }

        public static IEnumerable<Member> MembersByName(this IRepository<Member> repository, string gameName)
        {
            return repository
                .Queryable()
                .Where(c => c.Name.Contains(gameName))
                .ToList();
        }


        public static IEnumerable<Member> MembersByEmail(this IRepository<Member> repository, IRepository<UserAccount> userAccount, string Email)
        {
            return userAccount.Queryable()
                        .Where(usr => usr.Email.Equals(Email))
                        .Join(repository.Queryable(), usr => usr.Key, mem => mem.UserKey, (usr, mem) => mem).Where(m => m.IsActive == true)
                        .ToList();
        }

        public static Member FindMemberByEmail(this IRepository<Member> repository, IRepository<UserAccount> userAccount, string Email, int MemberId)
        {
            return userAccount.Queryable()
                        .Where(usr => usr.Email.Equals(Email))
                        .Join(repository.Queryable(), usr => usr.Key, mem => mem.UserKey, (usr, mem) => mem).Where(mem => mem.Key == MemberId && mem.IsActive == true)
                        .FirstOrDefault();

        }

        public static IEnumerable<Member> MembersAll(this IRepository<Member> repository)
        {
            return repository
                .Queryable()
                .ToList();
        }

       
    }
}