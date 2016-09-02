using Ninject;
using Ninject.Modules;

namespace Search.Infrastructure.Injection
{
    public class MainNinjectModule : NinjectModule
    {
        public override void Load()
        {
            var kernel = new StandardKernel();

            // Application
            Bind<Application.Abstract.Document.IFrontiersSearchDocumentService>().To<Application.Document.FrontiersSearchDocumentService>();

            // Infrastructure


            // Configuration
            Bind<Search.Infrastructure.Configuration.Abstract.Providers.IDatabaseProvider>().To<Search.Infrastructure.Configuration.Providers.DatabaseProvider>().InSingletonScope();
            Bind<Search.Infrastructure.Configuration.Abstract.Providers.IWebConfigProvider>().To<Search.Infrastructure.Configuration.Providers.WebConfigProvider>().InSingletonScope();
            Bind<Configuration.Abstract.IFrontiersConfigurationManager>().To<Search.Infrastructure.Configuration.FrontiersConfigurationManager>().InSingletonScope();
            Bind<Configuration.Abstract.Settings.IAddressSettings>().To<Search.Infrastructure.Configuration.Settings.AddressSettings>().InSingletonScope();

            // Logger
            Bind<Search.Infrastructure.Logging.Abstract.ILogger>().To<Search.Infrastructure.Logging.NLog.NLogFacade>();
            Bind<Search.Infrastructure.Logging.NLog.ILogEventInfoFactory>().To<Search.Infrastructure.Logging.NLog.NLogEventInfoFactory>();

            // Security
            Bind<Search.Infrastructure.Security.Abstract.Cryptography.ICryptographicProvider>().To<Search.Infrastructure.Security.Cryptography.CryptographicProvider>().InSingletonScope();

            // DataAccess
            Bind<DataAccess.ElasticSearch.Core.Repository.IElasticSearchRepository>().To<DataAccess.ElasticSearch.Core.Repository.ElasticSearchRepository>().InSingletonScope();
        }
    }
}
