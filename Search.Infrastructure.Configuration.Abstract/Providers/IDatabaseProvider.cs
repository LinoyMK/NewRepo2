namespace Search.Infrastructure.Configuration.Abstract.Providers
{
    public interface IDatabaseProvider
    {
        T Get<T>(string key);
    }
}
