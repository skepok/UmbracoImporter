[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UmbracoImporter.Core.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UmbracoImporter.Core.App_Start.NinjectWebCommon), "Stop")]

namespace UmbracoImporter.Core.App_Start
{
	using System;
	using System.Web;

	using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	using Ninject;
	using Ninject.Web.Common;
	using Importers;
	using Interfaces;
	using System.Web.Http;
	using Ninject.Activation;
	using Ninject.Syntax;
	using System.Web.Http.Dependencies;
	using System.Collections.Generic;
	using System.Linq;
	using Ninject.Parameters;
	using Umbraco.Core.Services;
	using Umbraco.Core.Persistence.UnitOfWork;

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

				GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
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
			//kernel.Bind<IDatabaseUnitOfWorkProvider>().To<PetaPocoUnitOfWorkProvider>().InRequestScope();
			//kernel.Bind<IContentTypeService>().To<ContentTypeService>();
			
			kernel.Bind<IDocumentTypeImporter>().To<DocumentTypeImporter>();
		}        
    }

	public class NinjectResolver : NinjectScope, IDependencyResolver
	{
		private IKernel _kernel;
		public NinjectResolver(IKernel kernel) : base(kernel)
		{
			_kernel = kernel;
		}
		public IDependencyScope BeginScope()
		{
			return new NinjectScope(_kernel.BeginBlock());
		}
	}

	public class NinjectScope : IDependencyScope
	{
		protected IResolutionRoot resolutionRoot;
		public NinjectScope(IResolutionRoot kernel)
		{
			resolutionRoot = kernel;
		}
		public object GetService(Type serviceType)
		{
			IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
			return resolutionRoot.Resolve(request).SingleOrDefault();
		}
		public IEnumerable<object> GetServices(Type serviceType)
		{
			IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
			return resolutionRoot.Resolve(request).ToList();
		}
		public void Dispose()
		{
			IDisposable disposable = (IDisposable)resolutionRoot;
			if (disposable != null) disposable.Dispose();
			resolutionRoot = null;
		}

		//IEnumerable<object> IDependencyScope.GetServices(Type serviceType)
		//{
		//	return serviceType.
		//}
	}
}
