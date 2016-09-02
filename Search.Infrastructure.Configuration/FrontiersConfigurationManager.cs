using Search.Infrastructure.Configuration.Abstract;
using Search.Infrastructure.Configuration.Abstract.Settings;

namespace Search.Infrastructure.Configuration
{
    public class FrontiersConfigurationManager : IFrontiersConfigurationManager
    {
        #region PRIVATE FIELDS

        private readonly IAddressSettings _addressSettings;

        #endregion

        #region CONTRUCTOR

        public FrontiersConfigurationManager(IAddressSettings addressSettings)
        {
            _addressSettings = addressSettings;
        }

        #endregion

        #region PUBLIC PROPERTIES

        public IAddressSettings AddressSettings
        {
            get { return _addressSettings; }

        }

        #endregion
    }
}
