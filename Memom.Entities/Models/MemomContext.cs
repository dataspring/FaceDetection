using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Memom.Entities.Models.Mapping;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
    public partial class MemomContext : DataContext
    {
        static MemomContext()
        {
            Database.SetInitializer<MemomContext>(null);
        }

        public MemomContext()
            : base("Name=MemomContext")
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserAlbumInstanceDetail> UserAlbumInstanceDetails { get; set; }
        public DbSet<UserAlbumInstance> UserAlbumInstances { get; set; }
        public DbSet<UserAlbumStatusChange> UserAlbumStatusChanges { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserLogInAttempt> UserLogInAttempts { get; set; }
        public DbSet<UserPasswordChange> UserPasswordChanges { get; set; }
        public DbSet<UserPasswordReset> UserPasswordResets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlbumMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new MemberMap());
            modelBuilder.Configurations.Add(new UserAccountMap());
            modelBuilder.Configurations.Add(new UserAlbumInstanceDetailMap());
            modelBuilder.Configurations.Add(new UserAlbumInstanceMap());
            modelBuilder.Configurations.Add(new UserAlbumStatusChangeMap());
            modelBuilder.Configurations.Add(new UserGroupMap());
            modelBuilder.Configurations.Add(new UserLogInAttemptMap());
            modelBuilder.Configurations.Add(new UserPasswordChangeMap());
            modelBuilder.Configurations.Add(new UserPasswordResetMap());
        }
    }
}
