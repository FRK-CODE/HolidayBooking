using System.Web.Http;
using Ninject;
using Ninject.Web.Common;
using WebApiContrib.IoC.Ninject;
using Ninject.Extensions.Conventions;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web;
using GWHolidayBookingWeb.Models.Contexts;
using GWHolidayBookingWeb.Models.Services;
using GWHolidayBookingWeb.Models.Repositorys;
using System;
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GWHolidayBookingWeb.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GWHolidayBookingWeb.App_Start.NinjectWebCommon), "Stop")]

namespace GWHolidayBookingWeb.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Kernel
        {
            get { return bootstrapper.Kernel; }
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
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
            kernel.Bind<IUserContext>().To<UserContext>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IAuthService>().To<AuthService>().InRequestScope();
            kernel.Bind<IAuthRepository>().To<AuthRepository>().InRequestScope();
            kernel.Bind<IAuthContext>().To<AuthContext>().InRequestScope();
        }
    }
}
