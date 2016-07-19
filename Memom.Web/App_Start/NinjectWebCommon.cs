[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MemomMvc52.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(MemomMvc52.App_Start.NinjectWebCommon), "RegisterRoleProvider")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MemomMvc52.App_Start.NinjectWebCommon), "Stop")]

namespace MemomMvc52.App_Start
{

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc.FilterBindingSyntax;
    using System;
    using System.Data.Entity;
    using System.Web;
    using System.Web.Mvc;
    using MemomMvc52.Areas.UserAccount;
    using Memom.Service;
    using MemomMvc52.Utilities;
    using Repository.Pattern.DataContext;
    using Memom.Entities.Models;
    using Repository.Pattern.Ef6.Factories;
    using Repository.Pattern.UnitOfWork;
    using Repository.Pattern.Ef6;
    using Repository.Pattern.Repositories;
    using System.Web.Security;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void RegisterRoleProvider()
        {
            bootstrapper.Kernel.Inject(Roles.Provider);
        } 
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //var config = MembershipRebootConfig.Create();
            //kernel.Bind<UserAccountService>().ToSelf();
            //kernel.Bind<MembershipRebootConfiguration<CustomUserAccount>>().ToConstant(config);
            //kernel.Bind<IUserAccountRepository<CustomUserAccount>>().To<CustomRepository>().InRequestScope();
            //kernel.Bind<IUserAccountQuery>().To<CustomRepository>().InRequestScope();
            //kernel.Bind<AuthenticationService<CustomUserAccount>>().To<SamAuthenticationService<CustomUserAccount>>();

            kernel.Bind<IDataContextAsync>().To<MemomContext>().InRequestScope();
            kernel.Bind<IRepositoryProvider>()
                .To<RepositoryProvider>()
                .InRequestScope()
                .WithConstructorArgument(new object[] { new RepositoryFactories() });

            kernel.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IRepositoryAsync<Album>>().To<Repository<Album>>().InRequestScope();
            kernel.Bind<IRepositoryAsync<UserAlbumInstance>>().To<Repository<UserAlbumInstance>>().InRequestScope();
            kernel.Bind<IRepositoryAsync<UserAccount>>().To<Repository<UserAccount>>().InRequestScope();
            kernel.Bind<IRepositoryAsync<Member>>().To<Repository<Member>>().InRequestScope();
            kernel.Bind<IAppDbStoredProcedures>().To<MemomContext>().InRequestScope();

            kernel.Bind<IAlbumService>().To<AlbumService>().InRequestScope();
            kernel.Bind<IAlbumInstanceService>().To<AlbumInstanceService>().InRequestScope();
            kernel.Bind<IMemberService>().To<MemberService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            //kernel.BindFilter<AdminAuthorizeAttribute>(FilterScope.Controller,0).WhenControllerHas<AdminAuthorizeAttribute>();
            kernel.Bind<System.Web.Security.RoleProvider>().To<WdaRoleProvider>().InRequestScope();
            //kernel.Bind<RoleProvider>().ToMethod(ctx => Roles.Provider);
            //kernel.Bind<IHttpModule>().To<ProviderInitializationHttpModule>();
       

            //kernel.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();
            //kernel.Bind<IRepositoryAsync<Album>>().To<Repository<Album>>().InRequestScope();





        }

    }
}
