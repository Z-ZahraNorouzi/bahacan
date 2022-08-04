namespace Service.Security
{
    public interface ISessionManager
    {
        T GetComplexData<T>(string key);

        void SetComplexData(string key, object value);
    }
}
