using Search.Infrastructure.Configuration.Abstract.Settings;

namespace Search.Infrastructure.Configuration.Abstract
{
    public interface IFrontiersConfigurationManager
    {
        IAddressSettings AddressSettings { get; }
    }
}
