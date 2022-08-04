using System;
using System.Collections.Generic;
using System.Text;

namespace StaticData
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresTime { get; set; }

    }
}
