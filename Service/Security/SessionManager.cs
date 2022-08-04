using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Service.Security
{
    public class SessionManager: ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public SessionManager(HttpContext httpContext)
        {
            _session = httpContext.Session;
        }

        public T GetComplexData<T>(string key)
        {
            var data = _session.GetString(key);
            return data == null ? default : JsonConvert.DeserializeObject<T>(data);
        }

        public void SetComplexData(string key, object value)
        {
            _session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
