using System.Collections.Generic;

namespace Search.Infrastructure.Configuration.Abstract.Providers
{
    public interface IWebConfigProvider
    {
        T GetAppSetting<T>(string key);

        Dictionary<string, string> GetAllAppSettings();

        T GetConnectionString<T>(string key);

        Dictionary<string, string> GetAllConnectionStrings();
    }
}
