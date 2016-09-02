using Search.Infrastructure.Configuration.Abstract.Providers;
using Search.Infrastructure.Configuration.Abstract.Settings;

namespace Search.Infrastructure.Configuration.Settings
{
    public class AddressSettings : IAddressSettings
    {
        #region PRIVATE INSTANCE FIELDS

        private readonly IWebConfigProvider _webConfigProvider;
        private readonly IDatabaseProvider _databaseProvider;

        #endregion

        #region CONSTRUCTORS

        public AddressSettings(IWebConfigProvider webConfigProvider, IDatabaseProvider databaseProvider)
        {
            _webConfigProvider = webConfigProvider;
            _databaseProvider = databaseProvider;
        }

        #endregion

        #region PUBLIC PROPERTIES

        public string JournalUIAddress
        {
            get { return _webConfigProvider.GetAppSetting<string>("Journal:UI:URL"); }
        }


        public string LoopWebsiteUrl
        {
            get { return _webConfigProvider.GetAppSetting<string>("Loop:UI:URL"); }
        }

        #endregion
    }
}
